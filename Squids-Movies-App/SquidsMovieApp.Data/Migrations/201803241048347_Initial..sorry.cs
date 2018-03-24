namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialsorry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Plot = c.String(maxLength: 1000),
                        Year = c.Int(),
                        Runtime = c.Int(),
                        Rated = c.String(),
                        Price = c.Double(nullable: false),
                        ImdbRating = c.Double(nullable: false),
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
                        MoneyBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        Bio = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ParticipantId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        Rating = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.MoviePosters",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Poster = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        ParticipantId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.ParticipantId)
                .Index(t => t.MovieId);
            
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
                "dbo.ParticipantsLikedBy",
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
                "dbo.MovieGenres",
                c => new
                    {
                        Movie_MovieId = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Genre_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Genre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Roles", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviePosters", "Id", "dbo.Movies");
            DropForeignKey("dbo.MovieGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.MovieGenres", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.ParticipantsLikedBy", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.ParticipantsLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.ParticipantMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.ParticipantMovies", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.MoviesLikedBy", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.MoviesBoughtBy", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesBoughtBy", "UserId", "dbo.Users");
            DropIndex("dbo.MovieGenres", new[] { "Genre_Id" });
            DropIndex("dbo.MovieGenres", new[] { "Movie_MovieId" });
            DropIndex("dbo.ParticipantsLikedBy", new[] { "ParticipantId" });
            DropIndex("dbo.ParticipantsLikedBy", new[] { "UserId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.MoviesLikedBy", new[] { "MovieId" });
            DropIndex("dbo.MoviesLikedBy", new[] { "UserId" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId1" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId" });
            DropIndex("dbo.MoviesBoughtBy", new[] { "MovieId" });
            DropIndex("dbo.MoviesBoughtBy", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "MovieId" });
            DropIndex("dbo.Roles", new[] { "ParticipantId" });
            DropIndex("dbo.MoviePosters", new[] { "Id" });
            DropIndex("dbo.Reviews", new[] { "MovieId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropTable("dbo.MovieGenres");
            DropTable("dbo.ParticipantsLikedBy");
            DropTable("dbo.ParticipantMovies");
            DropTable("dbo.MoviesLikedBy");
            DropTable("dbo.UserUsers");
            DropTable("dbo.MoviesBoughtBy");
            DropTable("dbo.Roles");
            DropTable("dbo.MoviePosters");
            DropTable("dbo.Reviews");
            DropTable("dbo.Participants");
            DropTable("dbo.Users");
            DropTable("dbo.Movies");
            DropTable("dbo.Genres");
        }
    }
}
