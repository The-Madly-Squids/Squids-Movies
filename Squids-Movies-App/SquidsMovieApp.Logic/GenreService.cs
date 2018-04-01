using AutoMapper;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Logic
{
    public class GenreService : IGenreService
    {
        private readonly IMovieAppDBContext movieAppDbContext;
        private readonly IMapper mapper;

        public GenreService(IMovieAppDBContext movieAppDbContext, IMapper mapper)
        {
            this.movieAppDbContext = movieAppDbContext;
            this.mapper = mapper;
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            var genres = this.movieAppDbContext.Genres.ToList();

            var genresDto = this.mapper.Map<IList<GenreModel>>(genres);

            return genresDto;
        }

        public GenreModel GetGenreDto(string genreName)
        {
            var genre = this.movieAppDbContext.Genres.Where(g => g.GenreType == genreName).FirstOrDefault();

            if (genre == null)
            {
                throw new ArgumentNullException("Genre not found!");
            }

            var genreDto = this.mapper.Map<GenreModel>(genre);

            return genreDto;
        }
    }
}
