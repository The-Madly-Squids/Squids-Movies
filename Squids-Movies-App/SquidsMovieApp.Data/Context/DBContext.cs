﻿using SquidsMovieApp.Data.Models;
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
            : base("name=MovieAppContext")
        {
            Database.SetInitializer<DBContext>(new CreateDatabaseIfNotExists<DBContext>());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

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
               .HasMany(u => u.LikedActors)
               .WithMany(a => a.ActorLikedBy)
               .Map(mc =>
               {
                   mc.MapLeftKey("UserId");
                   mc.MapRightKey("ParticipantId");
                   mc.ToTable("ActorsLikedBy");
               });

            modelBuilder.Entity<User>()
               .HasMany(u => u.LikedDirectors)
               .WithMany(d => d.DirectorLikedBy)
               .Map(mc =>
               {
                   mc.MapLeftKey("UserId");
                   mc.MapRightKey("ParticipantId");
                   mc.ToTable("DirectorsLikedBy");
               });


            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviews);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Movie)
                .WithMany(u => u.Reviews);
        }
    }
}
