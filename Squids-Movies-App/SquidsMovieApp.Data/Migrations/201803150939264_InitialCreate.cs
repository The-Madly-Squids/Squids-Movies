namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        User_UserId = c.Int(),
                        User_UserId1 = c.Int(),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ParticipantId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        User_UserId = c.Int(),
                        User_UserId1 = c.Int(),
                    })
                .PrimaryKey(t => t.ParticipantId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ReviewId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Nickname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        MoneyBalance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.Participants", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.Participants", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Movies", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ParticipantMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.ParticipantMovies", "Participant_ParticipantId", "dbo.Participants");
            DropIndex("dbo.UserUsers", new[] { "User_UserId1" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Participants", new[] { "User_UserId1" });
            DropIndex("dbo.Participants", new[] { "User_UserId" });
            DropIndex("dbo.Movies", new[] { "User_UserId1" });
            DropIndex("dbo.Movies", new[] { "User_UserId" });
            DropTable("dbo.UserUsers");
            DropTable("dbo.ParticipantMovies");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Participants");
            DropTable("dbo.Movies");
        }
    }
}
