using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetAnalysisTool.Shared.Models
{
    public class Stat
    {
        public int Id { get; set; }
        public int Ast { get; set; }  // Assists
        public int Blk { get; set; }  // Blocks
        public int Dreb { get; set; }  // Defensive rebounds
        public double Fg3Pct { get; set; }  // 3-point %
        public int Fg3a { get; set; }  // 3-point attempts
        public int Fg3m { get; set; }  // 3-point makes
        public double FgPct { get; set; }  // Field goal %
        public int Fga { get; set; }  // Field goal attempts
        public int Fgm { get; set; }  // Field goal makes
        public double FtPct { get; set; }  // Free throw %
        public int Fta { get; set; }  // Free throw attempts
        public int Ftm { get; set; }  // Free throw makes
        public string Min { get; set; }  // Minutes played, e.g., "35:12"
        public int Oreb { get; set; }  // Offensive rebounds
        public int Pf { get; set; }  // Personal fouls
        public int Pts { get; set; }  // Points
        public int Reb { get; set; }  // Total rebounds
        public int Stl { get; set; }  // Steals
        public int Turnover { get; set; }  // Turnovers (often 'to' in API)
        public Game Game { get; set; }  // Nested Game
        public Player Player { get; set; }  // Nested Player
        public Team Team { get; set; }  // Nested Team
    }
}
