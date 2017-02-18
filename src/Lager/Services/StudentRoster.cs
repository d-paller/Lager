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
                List<Student> list = new List<Student>();
                while (csvReader.Read())
                {
                    Student s = new Student();
                    string temp = "";
                    int tempint = 0;

                    if (csvReader.TryGetField<string>("Student", out temp))
                        s.Name = temp;
                    if (csvReader.TryGetField("ID", out tempint))
                        s.Id = tempint;
                    if (csvReader.TryGetField("SIS Login ID", out temp))
                        s.SisLoginId = temp;
                    if (csvReader.TryGetField("Section", out temp))
                        s.Section = temp;

                    list.Add(s);
                }

                await _studentRepo.AddAllAsync(list);
            }

            
        }

    }
}
