using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lager.Models;
using Microsoft.Extensions.Options;

namespace Lager.Services.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DBContext _context = null;

        public StudentRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task AddAll(IEnumerable<Student> students)
        {

            await _context.Students.InsertManyAsync(students);
        }
    }
}
