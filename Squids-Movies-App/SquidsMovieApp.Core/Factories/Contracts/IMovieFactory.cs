using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Core.Factories.Contracts
{
    public interface IMovieModelFactory
    {
        MovieModel CreateMovieModel(string name, string description, int year,
                    int runningTime);
    }
}
