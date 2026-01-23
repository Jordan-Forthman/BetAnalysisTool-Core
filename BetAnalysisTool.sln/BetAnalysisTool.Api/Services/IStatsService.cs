using BetAnalysisTool.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetAnalysisTool.Api.Services;

public interface IStatsService
{
    Task<List<Team>> GetTeamsAsync();  // Non-paginated, as per API

    Task<PagedResponse<Player>> GetPlayersAsync(string search = null, int perPage = 25, int page = 1);  // Paginated: use 'page' for simplicity (API uses 'page')

    Task<PagedResponse<Game>> GetGamesAsync(DateTime? startDate = null, DateTime? endDate = null, int[] teamIds = null, bool postseason = false, int perPage = 25, int page = 1);  // Paginated: flexible filters

    Task<PagedResponse<Stat>> GetStatsAsync(int[] playerIds = null, int[] gameIds = null, int[] seasons = null, DateTime? startDate = null, DateTime? endDate = null, bool postseason = false, int perPage = 25, int page = 1);  // Paginated: comprehensive for analysis

    // Extend later for averages (/season_averages) or other endpoints if needed for projections
}
