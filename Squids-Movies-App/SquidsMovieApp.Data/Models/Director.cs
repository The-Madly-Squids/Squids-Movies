using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models.Abstract;

namespace SquidsMovieApp.Data.Models
{
    public class Director : Participant
    {
        public Director(string firstName, string lastName, int age, int rating) :
            base(firstName, lastName, age, rating)
        {
            this.Movies = new HashSet<Movie>();
        }

        // add them to abstract class ??
        public ICollection<Movie> Movies { get; set; }

    }
}
