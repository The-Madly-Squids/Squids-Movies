using SquidsMovieApp.Models;
using System.Data.Common;
using System.Data.Entity;

namespace SquidsMovieApp.Data.Context
{
    public class MovieAppDBContext : DbContext, IMovieAppDBContext
    {
        public MovieAppDBContext()
            : base("name=MovieAppContext")
        {
            Database.SetInitializer<MovieAppDBContext>(new CreateDatabaseIfNotExists<MovieAppDBContext>());
        }

        public MovieAppDBContext(DbConnection connection)
           : base(connection, true)
        {
        }

        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Participant> Participants { get; set; }
        public IDbSet<Review> Reviews { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Genre> Genres { get; set; }
        public IDbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(u => u.BoughtMovies)
               .WithMany(m => m.BoughtBy)
               .Map(mc =>
               {
                   mc.MapLeftKey("UserId");
                   mc.MapRightKey("MovieId");
                   mc.ToTable("MoviesBoughtBy");
               });

            modelBuilder.Entity<User>()
               .HasMany(u => u.LikedMovies)
               .WithMany(m => m.LikedBy)
               .Map(mc =>
               {
                   mc.MapLeftKey("UserId");
                   mc.MapRightKey("MovieId");
                   mc.ToTable("MoviesLikedBy");
               });

            modelBuilder.Entity<User>()
               .HasMany(u => u.LikedParticipants)
               .WithMany(a => a.ParticipantLikedByUser)
               .Map(mc =>
               {
                   mc.MapLeftKey("UserId");
                   mc.MapRightKey("ParticipantId");
                   mc.ToTable("ParticipantsLikedBy");
               });

            //modelBuilder.Entity<User>()
            //   .HasMany(u => u.LikedDirectors)
            //   .WithMany(d => d.DirectorLikedBy)
            //   .Map(mc =>
            //   {
            //       mc.MapLeftKey("UserId");
            //       mc.MapRightKey("ParticipantId");
            //       mc.ToTable("DirectorsLikedBy");
            //   });


            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviews);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Movie)
                .WithMany(u => u.Reviews);
        }
    }
}
