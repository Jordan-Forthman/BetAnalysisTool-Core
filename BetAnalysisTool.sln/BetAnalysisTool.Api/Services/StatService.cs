using BetAnalysisTool.Shared.Models;
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
    private readonly IHttpClientFactory _clientFactory; // Injected factory for creating named HttpClients.
    private readonly IMemoryCache _cache; // Injected cache for storing responses: Reduces redundant API hits.

    // Constructor for DI (Dependency Injection): Ensures service is loosely coupled, testable by mocking dependencies.
    public StatsService(IHttpClientFactory clientFactory, IMemoryCache cache)
    {
        _clientFactory = clientFactory;
        _cache = cache;
    }

    // Fetches all NBA teams (non-paginated endpoint).
    public async Task<List<Team>> GetTeamsAsync()
    {
        const string cacheKey = "BallDontLie_Teams"; // Unique endpoint key prevents collisions
        if (!_cache.TryGetValue(cacheKey, out List<Team> teams)) // Cache hit check
        {
            var client = _clientFactory.CreateClient("BallDontLieClient"); // Name client
            var response = await client.GetFromJsonAsync<PagedResponse<Team>>("/teams"); // Direct deserialization to model
            teams = response?.Data ?? new List<Team>(); // Handle API errors on empty list via null-coalescense 
            _cache.Set(cacheKey, teams, TimeSpan.FromHours(24));  // Long cache: teams stats dont change often.
        }
        return teams;
    }

    // Fetch paginated players with optional search params.
    public async Task<PagedResponse<Player>> GetPlayersAsync(string search = null, int perPage = 25, int page = 1)
    {
        var queryParams = new Dictionary<string, string>  // Dict for flexible param collection.
        {
            { "search", search },
            { "per_page", perPage.ToString() },
            { "page", page.ToString() }
        };
        var query = BuildQueryString(queryParams);  // Helper converts dict to "?key=value&..."
        var cacheKey = $"BallDontLie_Players_{query}";  // Includes query in key for cache specificity
        if (!_cache.TryGetValue(cacheKey, out PagedResponse<Player> response))
        {
            var client = _clientFactory.CreateClient("BallDontLieClient");
            response = await client.GetFromJsonAsync<PagedResponse<Player>>($"/players?{query}");  // Appends query to endpoint
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(30));  // Medium cache: players stats change occasionally.
        }
        return response ?? new PagedResponse<Player> { Data = new List<Player>(), Meta = new Meta() };  // Fallback to empty for error resilience.
    }

    // Fetches paginated games with filters (dates, teams, post-season).
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
        if (teamIds != null && teamIds.Length > 0)
        {
            foreach (var id in teamIds) queryParams.Add("team_ids[]", id.ToString()); // Multi-add for array params. API expects array params as repeated keys.
        }
        var query = BuildQueryString(queryParams);
        var cacheKey = $"BallDontLie_Games_{query}";
        if (!_cache.TryGetValue(cacheKey, out PagedResponse<Game> response))
        {
            var client = _clientFactory.CreateClient("BallDontLieClient");
            response = await client.GetFromJsonAsync<PagedResponse<Game>>($"/games?{query}");
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));  // Short cache: game dynamics change frequently.
        }
        return response ?? new PagedResponse<Game> { Data = new List<Game>(), Meta = new Meta() };
    }

    /// Fetches paginated stats with extended filters (players, games, seasons, dates). ** Enables matchup analysis. **
    public async Task<PagedResponse<Stat>> GetStatsAsync(int[] playerIds = null, int[] gameIds = null, int[] seasons = null, DateTime? startDate = null, DateTime? endDate = null, bool postseason = false, int perPage = 25, int page = 1)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "per_page", perPage.ToString() },
            { "page", page.ToString() },
            { "postseason", postseason ? "true" : null }
        };
        if (startDate.HasValue) queryParams["start_date"] = startDate.Value.ToString("yyyy-MM-dd");
        if (endDate.HasValue) queryParams["end_date"] = endDate.Value.ToString("yyyy-MM-dd");
        if (playerIds != null) foreach (var id in playerIds) queryParams.Add("player_ids[]", id.ToString());
        if (gameIds != null) foreach (var id in gameIds) queryParams.Add("game_ids[]", id.ToString());
        if (seasons != null) foreach (var season in seasons) queryParams.Add("seasons[]", season.ToString());
        var query = BuildQueryString(queryParams);
        var cacheKey = $"BallDontLie_Stats_{query}";
        if (!_cache.TryGetValue(cacheKey, out PagedResponse<Stat> response))
        {
            var client = _clientFactory.CreateClient("BallDontLieClient");
            response = await client.GetFromJsonAsync<PagedResponse<Stat>>($"/stats?{query}");
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));  // Short cache: stats dynamic
        }
        return response ?? new PagedResponse<Stat> { Data = new List<Stat>(), Meta = new Meta() };
    }

    // Helper to build URL-encoded query string from dict. Handle special chars/arrays properly. Skip null/empty values. Supports multi-value key (E.g. ids[]).
    private string BuildQueryString(Dictionary<string, string> paramsDict)
    {
        if (paramsDict == null || paramsDict.Count == 0) return string.Empty; // Early return for no params

        var query = HttpUtility.ParseQueryString(string.Empty); // Create empty query collection
        foreach (var kvp in paramsDict)
        {
            if (!string.IsNullOrEmpty(kvp.Value)) query[kvp.Key] = kvp.Value;
        }
        return query.ToString(); // Output encoded string (E.g. "key=value&ids[]=1&ids[]=2").
    }
}
