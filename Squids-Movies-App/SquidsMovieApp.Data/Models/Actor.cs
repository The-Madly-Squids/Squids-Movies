using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Contracts;
using SquidsMovieApp.Data.Models.Abstract;

namespace SquidsMovieApp.Data.Models
{
    public class Actor : Participant
    {
        public Actor(string firstName, string lastName, int age, int rating) :
            base(firstName, lastName, age, rating)
        {
            this.Movies = new HashSet<Movie>();
        }

        public ICollection<Movie> Movies { get; set; }

    }
}
