using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Contracts
{
    public interface IMovie
    {
        string Name { get; set; }
        string Description { get; set }
        int Year { get; set; }
        int Rating { get; set; }
        int RunningTime { get; set; }

    }
}
