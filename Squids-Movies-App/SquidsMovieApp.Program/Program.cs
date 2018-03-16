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
            using (var ctx = new MovieAppDBContext())
            {
                var movie = new Movie()
                {
                    Name = "Terminator I",
                    Description = "Fantastic future movie"
                };

                var movie2 = new Movie()
                {
                    Name = "Terminator II",
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

                var user2 = new User()
                {
                    FirstName = "Pesho2"
                };

                //user.LikedMovies.Add(movie);
                //user.LikedMovies.Add(movie2);
                //user2.LikedMovies.Add(movie);
                //user2.LikedMovies.Add(movie2);
                user.LikedActors.Add(actor);
                movie.Participants.Add(actor);

                ctx.Movies.Add(movie);
                ctx.Movies.Add(movie2);
                ctx.Users.Add(user);
                ctx.Users.Add(user2);
                ctx.Participants.Add(actor);

                ctx.SaveChanges();
            }
        }
    }
}
