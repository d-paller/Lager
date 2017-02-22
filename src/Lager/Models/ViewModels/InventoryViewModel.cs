using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Models.ViewModels
{
    public class InventoryViewModel
    {
        public PagingInfo PagingInfo { get; set; }

        public IEnumerable<Part> Parts { get; set; }

        public Search search { get; set; }
    }
}
