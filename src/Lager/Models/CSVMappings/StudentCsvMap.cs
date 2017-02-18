using CsvHelper.Configuration;
using Lager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Models.CSVMappings
{
    public class StudentCsvMap :CsvClassMap<Student>
    {
        public StudentCsvMap()
        {
            Map(x => x.Name).Name("Student");
            Map(x => x.Id).Name("ID");
            Map(x => x.SisLoginId).Name("SIS Login ID");
            Map(x => x.Section).Name("Section");
        }
    }
}
