using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Program
{
    class Program
    {
        static void Main()
        {
            using (var ctx = new DBContext())
            {
                var movie = new Movie()
                {
                    Name = "Terminator I",
                    Description = "Fantastic future movie"
                };


                var actor = new Participant()
                {
                    FirstName = "Arnold Schawarzenegger"
                };

                var user = new User()
                {
                    FirstName = "Pesho"
                };

                user.LikedMovies.Add(movie);
                user.LikedActors.Add(actor);
                movie.Participants.Add(actor);

                ctx.Movies.Add(movie);
                ctx.Users.Add(user);
                ctx.Participants.Add(actor);

                ctx.SaveChanges();
            }
        }
    }
}
