using SquidsMovieApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IGenreService
    {
        IEnumerable<GenreModel> GetAllGenres();
    }
}
