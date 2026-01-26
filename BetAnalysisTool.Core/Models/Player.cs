using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetAnalysisTool.Core.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }  // e.g., "LeBron"
        public string LastName { get; set; }  // e.g., "James"
        public string Position { get; set; }  // e.g., "F"
        public int? HeightFeet { get; set; }  // e.g., 6 (nullable for incomplete data)
        public int? HeightInches { get; set; }  // e.g., 9 (nullable for incomplete data)
        public int? WeightPounds { get; set; }  // e.g., 250 (nullable for incomplete data)
        public Team Team { get; set; }  // Nested Team object
    }
}
