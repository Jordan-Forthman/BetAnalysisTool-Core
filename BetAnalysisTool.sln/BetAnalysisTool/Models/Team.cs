using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetAnalysisTool.Shared.Models;

public class Team
{
    public int Id { get; set; }
    public string Abbreviation { get; set; }  // e.g., "LAL"
    public string City { get; set; }  // e.g., "Los Angeles"
    public string Conference { get; set; }  // e.g., "West"
    public string Division { get; set; }  // e.g., "Pacific"
    public string FullName { get; set; }  // e.g., "Los Angeles Lakers"
    public string Name { get; set; }  // e.g., "Lakers"
}
