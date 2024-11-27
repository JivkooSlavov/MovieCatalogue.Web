using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data
{
    public class SearchService : BaseService, ISearchService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;

        public SearchService(IRepository<Movie, Guid> movieRepository)
        {
            _movieRepository = movieRepository;   
        }
        public async Task<IEnumerable<MovieSearchResultViewModel>> SearchMoviesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<MovieSearchResultViewModel>();
            }

            return await _movieRepository
                .GetAllWithInclude(m => m.Genre)
                .Where(m => m.Title.ToLower().Contains(query.ToLower()) && !m.IsDeleted)
                .Select(m => new MovieSearchResultViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre.Name,
                    PosterUrl = m.PosterUrl,
                    Director = m.Director
                })
                .ToListAsync();
        }
    }
}
