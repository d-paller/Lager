using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Models.ViewModels
{
    public class RosterViewModel
    {
        public IEnumerable<IEnumerable<Student>> ListBySections { get; set; }

        public string Section { get; set; }
    }
}
