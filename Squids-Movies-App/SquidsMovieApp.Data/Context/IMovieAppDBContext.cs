using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.Data.Context
{
    public interface IMovieAppDBContext
    {
        IDbSet<Movie> Movies { get; set; }
        IDbSet<Participant> Participants { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<Review> Reviews { get; set; }
        IDbSet<Genre> Genres { get; set; }
        IDbSet<Role> Roles { get; set; }
        int SaveChanges();
    }
}
