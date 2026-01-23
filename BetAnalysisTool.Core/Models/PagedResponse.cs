using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

// Handles API's { "data": [], "meta": {} } structure. Use in services, e.g., PagedResponse<Player>.

namespace BetAnalysisTool.Core.Models
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; }
        public Meta Meta { get; set; }
    }
}
