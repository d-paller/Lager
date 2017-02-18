using Lager.Models;
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
    }
}
