using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.Data.Contracts
{
    public interface IParticipant
    {
        ICollection<Movie> Movies { get; set; }
    }
}
