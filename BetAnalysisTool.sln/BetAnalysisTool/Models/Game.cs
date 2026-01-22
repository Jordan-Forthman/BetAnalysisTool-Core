using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetAnalysisTool.Shared.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Date { get; set; }  // e.g., "2026-01-22"
        public int HomeTeamScore { get; set; }  // e.g., 110
        public int VisitorTeamScore { get; set; }  // e.g., 105
        public int Season { get; set; }  // e.g., 2025
        public int Period { get; set; }  // e.g., 3 (quarters)
        public string Status { get; set; }  // e.g., "Final" or "1:00 pm ET"
        public string Time { get; set; }  // e.g., "" (empty for final) or clock time
        public bool Postseason { get; set; }  // true for playoffs
        public Team HomeTeam { get; set; }  // Nested Team
        public Team VisitorTeam { get; set; }  // Nested Team
    }
}
