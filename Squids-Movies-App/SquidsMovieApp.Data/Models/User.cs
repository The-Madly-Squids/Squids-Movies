using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Contracts;
using SquidsMovieApp.Data.Models.Abstract;

namespace SquidsMovieApp.Data.Models
{
    public class User : Person, IUser
    {
        public User(string firstName, string lastName, int age)
            : base(firstName, lastName, age)
        {

        }
    }
}
