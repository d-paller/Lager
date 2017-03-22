using Lager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAllAsync(IEnumerable<Student> students);

        Task<IEnumerable<IEnumerable<Student>>> GetAllBySectionNumberAsync();

        Task<IEnumerable<Student>> GetStudents();
    }
}
