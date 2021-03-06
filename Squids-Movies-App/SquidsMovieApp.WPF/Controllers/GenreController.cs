﻿using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.WPF.Controllers
{
    public class GenreController
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            return this.genreService.GetAllGenres();
        }

        public GenreModel GetGenreDto(string genreName)
        {
            if (string.IsNullOrEmpty(genreName) || string.IsNullOrWhiteSpace(genreName))
            {
                throw new ArgumentNullException("Invalid genre name!");
            }

            return this.genreService.GetGenreDto(genreName);
        }
    }
}
