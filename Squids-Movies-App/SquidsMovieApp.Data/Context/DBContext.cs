using SquidsMovieApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Context
{
    public class DBContext : DbContext
    {
        public DBContext()
            :base("name=MovieAppContext")
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Participant> Participants  { get; set; }
        public DbSet<Review> Reviews  { get; set; }
        public DbSet<User> Users  { get; set; }
    }
}
