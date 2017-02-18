using CsvHelper;
using Lager.Interfaces;
using Lager.Models;
using Lager.Models.CSVMappings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Services
{
    public class StudentRoster
    {
        private IStudentRepository _studentRepo;
        public StudentRoster(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        /// <summary>
        /// Adds the csv at the given path to the Db
        /// </summary>
        /// <param name="path">The path to the csv file</param>
        /// <returns>Task</returns>
        public async Task AddStudentsToDb(string path)
        {

            using (TextReader reader = File.OpenText(path))
            {
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.RegisterClassMap<StudentCsvMap>();

                await _studentRepo.AddAll(csvReader.GetRecords<Student>().ToList());
            }

            
        }

    }
}
