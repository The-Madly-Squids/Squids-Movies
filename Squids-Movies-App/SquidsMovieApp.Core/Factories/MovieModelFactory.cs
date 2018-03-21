using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Core.Factories
{
    public class MovieModelFactory : IMovieModelFactory
    {
        public MovieModel CreateMovieModel(string name, string description, int year,
            int runningTime)
        {
            return new MovieModel()
            {
                Title = name,
                Plot = description,
                Year = year,
                Runtime = runningTime
            };
        }
    }
}
