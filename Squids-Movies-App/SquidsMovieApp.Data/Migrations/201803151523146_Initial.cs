namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 200),
                        Year = c.Int(),
                        Rating = c.Int(),
                        RunningTime = c.Int(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Age = c.Int(),
                        Nickname = c.String(maxLength: 30),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        MoneyBalance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ParticipantId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Age = c.Int(),
                    })
                .PrimaryKey(t => t.ParticipantId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        Movie_MovieId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviesBoughtBy",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        User_UserId1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.User_UserId1 })
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
            CreateTable(
                "dbo.ParticipantMovies",
                c => new
                    {
                        Participant_ParticipantId = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_ParticipantId, t.Movie_MovieId })
                .ForeignKey("dbo.Participants", t => t.Participant_ParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Participant_ParticipantId)
                .Index(t => t.Movie_MovieId);
            
            CreateTable(
                "dbo.ActorsLikedBy",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ParticipantId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ParticipantId);
            
            CreateTable(
                "dbo.DirectorsLikedBy",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ParticipantId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ParticipantId);
            
            CreateTable(
                "dbo.MoviesLikedBy",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_MovieId })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenreMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Reviews", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesLikedBy", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.DirectorsLikedBy", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.DirectorsLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActorsLikedBy", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.ActorsLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.ParticipantMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.ParticipantMovies", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.MoviesBoughtBy", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesBoughtBy", "UserId", "dbo.Users");
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropIndex("dbo.MoviesLikedBy", new[] { "MovieId" });
            DropIndex("dbo.MoviesLikedBy", new[] { "UserId" });
            DropIndex("dbo.DirectorsLikedBy", new[] { "ParticipantId" });
            DropIndex("dbo.DirectorsLikedBy", new[] { "UserId" });
            DropIndex("dbo.ActorsLikedBy", new[] { "ParticipantId" });
            DropIndex("dbo.ActorsLikedBy", new[] { "UserId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId1" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId" });
            DropIndex("dbo.MoviesBoughtBy", new[] { "MovieId" });
            DropIndex("dbo.MoviesBoughtBy", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "User_UserId" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieId" });
            DropTable("dbo.GenreMovies");
            DropTable("dbo.MoviesLikedBy");
            DropTable("dbo.DirectorsLikedBy");
            DropTable("dbo.ActorsLikedBy");
            DropTable("dbo.ParticipantMovies");
            DropTable("dbo.UserUsers");
            DropTable("dbo.MoviesBoughtBy");
            DropTable("dbo.Genres");
            DropTable("dbo.Reviews");
            DropTable("dbo.Participants");
            DropTable("dbo.Users");
            DropTable("dbo.Movies");
        }
    }
}
