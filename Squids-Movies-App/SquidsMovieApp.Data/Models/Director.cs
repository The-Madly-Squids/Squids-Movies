using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Contracts;
using SquidsMovieApp.Data.Models.Abstract;

namespace SquidsMovieApp.Data.Models
{
    // unite Actor/Director/Scenarist into participant class 
    public class Director : Person, IParticipant
    {
        public Director(string firstName, string lastName, int age, int rating) :
            base(firstName, lastName, age)
        {
            this.Movies = new HashSet<Movie>();
        }

        // add them to abstract class ??
        public ICollection<Movie> Movies { get; set; }

    }
}
