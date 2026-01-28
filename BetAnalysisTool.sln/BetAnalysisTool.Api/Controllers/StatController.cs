using BetAnalysisTool.Api.Services;  // For IStatsService
using BetAnalysisTool.Core.Models;   // For response models (PagedResponse<T>, etc.)
using Microsoft.AspNetCore.Authorization;  // For [Authorize]
using Microsoft.AspNetCore.Mvc;            // For ApiController, HttpGet, etc.
using System.Threading.Tasks;

namespace BetAnalysisTool.Api.Controllers;

[ApiController]                  // Enables model binding
[Route("api/[controller]")]      // Base route: /api/stats
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;  // Injected service for data access

    // Constructor: DI injects the service
    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    // GET /api/stats/teams
    // Returns all NBA teams (cached 24h in service)
    [HttpGet("teams")]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _statsService.GetTeamsAsync();
        return Ok(teams);
    }

    // GET /api/stats/players?search=lebron&perPage=10&page=1
    // Paginated players with optional search
    [HttpGet("players")]
    public async Task<IActionResult> GetPlayers(
        [FromQuery] string? search = null,
        [FromQuery] int perPage = 25,
        [FromQuery] int page = 1)
    {
        var response = await _statsService.GetPlayersAsync(search, perPage, page);
        return Ok(response);  // Returns PagedResponse<Player> with data + meta (total pages, etc.)
    }

    // GET /api/stats/games?startDate=2025-01-01&endDate=2025-01-31&teamIds=1,2&postseason=true&perPage=20&page=1
    // Filtered games for schedule/trends visualization
    [HttpGet("games")]
    public async Task<IActionResult> GetGames(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int[]? teamIds = null,
        [FromQuery] bool postseason = false,
        [FromQuery] int perPage = 25,
        [FromQuery] int page = 1)
    {
        var response = await _statsService.GetGamesAsync(startDate, endDate, teamIds, postseason, perPage, page);
        return Ok(response);
    }

    // GET /api/stats/stats?playerIds=237,278&seasons=2024&postseason=false&perPage=50&page=1
    // Detailed stats for matchup analysis, player comparisons, premium features
    [HttpGet("stats")]
    [Authorize]  // Restrict to authenticated/premium users (Auth0 JWT required)
    public async Task<IActionResult> GetStats(
        [FromQuery] int[]? playerIds = null,
        [FromQuery] int[]? gameIds = null,
        [FromQuery] int[]? seasons = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] bool postseason = false,
        [FromQuery] int perPage = 25,
        [FromQuery] int page = 1)
    {
        var response = await _statsService.GetStatsAsync(playerIds, gameIds, seasons, startDate, endDate, postseason, perPage, page);
        return Ok(response);
    }

    // Future extension point: Add [Authorize(Roles = "Premium")] if RBAC roles added in Auth0
}