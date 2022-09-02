using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IDirectorService : IDisposable
    {
        Task<IEnumerable<Director>> GetAll();
        Task<Director> GetById(int id);
        Task<Director> Add(Director director);
        Task<Director> Update(Director director);
        Task<bool> Remove(Director director);
        Task<IEnumerable<Director>> Search(string directorName);
    }
}
