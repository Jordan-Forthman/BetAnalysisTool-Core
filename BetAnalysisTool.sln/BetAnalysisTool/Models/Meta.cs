using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetAnalysisTool.Shared.Models
{
    public class Meta
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }  // Nullable if no next
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
    }
}
