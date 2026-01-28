using BetAnalysisTool.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace BetAnalysisTool.Api.Services;

public class StatsService : IStatsService
{
    private readonly HttpClient _httpClient; // CHANGED: Direct Client instead of Factory
    private readonly IMemoryCache _cache;

    // CHANGED: Constructor now accepts HttpClient directly
    // This matches the "AddHttpClient<IStatsService, StatsService>" in Program.cs
    public StatsService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<List<Team>> GetTeamsAsync()
    {
        const string cacheKey = "BallDontLie_Teams";

        if (!_cache.TryGetValue(cacheKey, out List<Team> teams))
        {
            // CHANGED: Use _httpClient directly. 
            // The BaseURL and API Key are already configured in Program.cs, so we just pass "teams"
            var response = await _httpClient.GetFromJsonAsync<PagedResponse<Team>>("teams");

            teams = response?.Data ?? new List<Team>();
            _cache.Set(cacheKey, teams, TimeSpan.FromHours(24));
        }
        return teams;
    }

    public async Task<PagedResponse<Player>> GetPlayersAsync(string search = null, int perPage = 25, int page = 1)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "search", search },
            { "per_page", perPage.ToString() },
            { "page", page.ToString() }
        };

        var query = BuildQueryString(queryParams);
        var cacheKey = $"BallDontLie_Players_{query}";

        if (!_cache.TryGetValue(cacheKey, out PagedResponse<Player> response))
        {
            // CHANGED: Use _httpClient directly
            // Note: Remove the "/" at the start of "players" to ensure it appends to the BaseAddress correctly
            response = await _httpClient.GetFromJsonAsync<PagedResponse<Player>>($"players?{query}");
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(30));
        }

        return response ?? new PagedResponse<Player> { Data = new List<Player>(), Meta = new Meta() };
    }

    public async Task<PagedResponse<Game>> GetGamesAsync(DateTime? startDate = null, DateTime? endDate = null, int[] teamIds = null, bool postseason = false, int perPage = 25, int page = 1)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "per_page", perPage.ToString() },
            { "page", page.ToString() },
            { "postseason", postseason ? "true" : null }
        };

        if (startDate.HasValue) queryParams["start_date"] = startDate.Value.ToString("yyyy-MM-dd");
        if (endDate.HasValue) queryParams["end_date"] = endDate.Value.ToString("yyyy-MM-dd");

        // Note: The logic for arrays needs to happen in BuildQueryString or handled manually here if your helper doesn't support it.
        // Assuming your BuildQueryString handles this loop or you add them manually:
        // (This part of your original logic was slightly tricky with Dictionary keys, but I've left it as is assuming your helper works)
        if (teamIds != null && teamIds.Length > 0)
        {
            // NOTE: Dictionaries can't have duplicate keys. 
            // If your API needs "team_ids[]=1&team_ids[]=2", a Dictionary<string,string> will overwrite the key.
            // See the note below the code block for a fix on this.
        }

        var query = BuildQueryString(queryParams);
        // Manually appending IDs if the helper didn't capture them (due to dictionary key uniqueness)
        if (teamIds != null)
        {
            foreach (var id in teamIds) query += $"&team_ids[]={id}";
        }

        var cacheKey = $"BallDontLie_Games_{query}";

        if (!_cache.TryGetValue(cacheKey, out PagedResponse<Game> response))
        {
            response = await _httpClient.GetFromJsonAsync<PagedResponse<Game>>($"games?{query}");
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
        }

        return response ?? new PagedResponse<Game> { Data = new List<Game>(), Meta = new Meta() };
    }

    public async Task<PagedResponse<Stat>> GetStatsAsync(int[] playerIds = null, int[] gameIds = null, int[] seasons = null, DateTime? startDate = null, DateTime? endDate = null, bool postseason = false, int perPage = 25, int page = 1)
    {
        // Basic params
        var queryParams = new Dictionary<string, string>
        {
            { "per_page", perPage.ToString() },
            { "page", page.ToString() },
            { "postseason", postseason ? "true" : null }
        };
        if (startDate.HasValue) queryParams["start_date"] = startDate.Value.ToString("yyyy-MM-dd");
        if (endDate.HasValue) queryParams["end_date"] = endDate.Value.ToString("yyyy-MM-dd");

        var query = BuildQueryString(queryParams);

        // Append array params manually to avoid Dictionary key collisions
        if (playerIds != null) foreach (var id in playerIds) query += $"&player_ids[]={id}";
        if (gameIds != null) foreach (var id in gameIds) query += $"&game_ids[]={id}";
        if (seasons != null) foreach (var s in seasons) query += $"&seasons[]={s}";

        var cacheKey = $"BallDontLie_Stats_{query}";

        if (!_cache.TryGetValue(cacheKey, out PagedResponse<Stat> response))
        {
            response = await _httpClient.GetFromJsonAsync<PagedResponse<Stat>>($"stats?{query}");
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
        }

        return response ?? new PagedResponse<Stat> { Data = new List<Stat>(), Meta = new Meta() };
    }

    // Kept your helper, but note the limitation on duplicate keys
    private string BuildQueryString(Dictionary<string, string> paramsDict)
    {
        if (paramsDict == null || paramsDict.Count == 0) return string.Empty;

        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var kvp in paramsDict)
        {
            if (!string.IsNullOrEmpty(kvp.Value)) query[kvp.Key] = kvp.Value;
        }
        return query.ToString();
    }
}