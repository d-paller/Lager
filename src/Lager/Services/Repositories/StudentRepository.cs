using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lager.Services.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DBContext _context = null;

        public StudentRepository(IOptions<Settings> settings)
        {
            _context = new DBContext(settings);
        }

        public async Task AddAllAsync(IEnumerable<Student> students)
        {
            await _context.Students.InsertManyAsync(students);

        }


        public async Task<IEnumerable<IEnumerable<Student>>> GetAllBySectionNumberAsync()
        {
            List<Student> sectionNumbers = await _context.Students.Find(_ => true).ToListAsync();
            List<List<Student>> list = new List<List<Student>>();

            for(int i = 0; i <sectionNumbers.Count; i++)
            {
                var filter = Builders<Student>.Filter.Eq("Section", sectionNumbers[i].Section );
                var newList = await _context.Students.Find(filter).ToListAsync();
                list.Add(newList);
            }

            return list;
        }


        public async Task<IEnumerable<Student>> GetStudents()
        {
            string section = await GetLatestSectionNumber();
            var filter = Builders<Student>.Filter.Eq("Section", section);
            var newList = await _context.Students.Find(filter).ToListAsync();

            return newList;
        }


        private async Task<string> GetLatestSectionNumber()
        {
            var all = await GetAllBySectionNumberAsync();
            var list = all.OrderByDescending(x=>x.FirstOrDefault().Section).ToList();
            return list.FirstOrDefault().FirstOrDefault().Section;
        }
        
    }
}
