using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IMovieService
    {
        IEnumerable<MovieModel> GetAllMovies();
        void AddMovie(MovieModel movie);
    }
}
