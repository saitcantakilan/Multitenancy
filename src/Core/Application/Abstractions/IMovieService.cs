using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IMovieService
    {
        Task<Movie> CreateAsync(string title, string description, float imdb);
        Task<Movie> GetByIdAsync(int id);
        Task<IReadOnlyList<Movie>> GetAllAsnyc();
    }
}
