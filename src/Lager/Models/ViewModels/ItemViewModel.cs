using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Models.ViewModels
{
    public class ItemViewModel
    {
        public Part Part { get; set; }

        public IEnumerable<Student> StudentSelectList { get; set; }
    }
}
