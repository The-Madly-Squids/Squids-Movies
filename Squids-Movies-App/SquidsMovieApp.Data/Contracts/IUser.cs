using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.Data.Contracts
{
    interface IUser
    {
        string Nickname { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        int MoneyBalance { get; set; }
        ICollection<Actor> LikedActors { get; set; }
        ICollection<Director> LikedDirectors { get; set; }
        ICollection<Movie> LikedMovies { get; set; }
        ICollection<Movie> BoughtMovies { get; set; }
        ICollection<User> Following { get; set; }
        ICollection<User> Followers { get; set; }

    }
}

