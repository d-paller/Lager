using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Models
{
    public class PagingInfo
    {
        public string SortField { get; set; }

        public bool SortDesc { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
