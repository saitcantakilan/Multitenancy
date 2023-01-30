using Application.Abstractions;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _movieService.GetAllAsnyc());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
            => Ok(await _movieService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateMovieVM createMovieVM)
            => Ok(await _movieService.CreateAsync(createMovieVM.Title, createMovieVM.Description, createMovieVM.IMDb));
    }
}
