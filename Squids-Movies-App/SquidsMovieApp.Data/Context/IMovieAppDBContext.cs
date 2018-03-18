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
        DbSet<Movie> Movies { get; set; }
        DbSet<Participant> Participants { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Genre> Genres { get; set; }
        int SaveChanges();
    }
}
