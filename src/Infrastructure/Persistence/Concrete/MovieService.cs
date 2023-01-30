using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Concrete
{
    public class MovieService : IMovieService
    {
        ApplicationDbContext _context;
        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> CreateAsync(string title, string description, float imdb)
        {
            Movie movie = new(title, description, imdb);
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<IReadOnlyList<Movie>> GetAllAsnyc()
            => await _context.Movies.ToListAsync();

        public async Task<Movie> GetByIdAsync(int id)
            => await _context.Movies.FindAsync(id);
    }
}
