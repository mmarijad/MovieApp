using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByName(string Name);
    }
}
