namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersToLikedAndBoughtMoviesAndUsersToLikedActorsAndDirectorsLinks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Participants", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Participants", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.Movies", "User_UserId1", "dbo.Users");
            DropIndex("dbo.Movies", new[] { "User_UserId" });
            DropIndex("dbo.Movies", new[] { "User_UserId1" });
            DropIndex("dbo.Participants", new[] { "User_UserId" });
            DropIndex("dbo.Participants", new[] { "User_UserId1" });
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
            
            DropColumn("dbo.Movies", "User_UserId");
            DropColumn("dbo.Movies", "User_UserId1");
            DropColumn("dbo.Participants", "User_UserId");
            DropColumn("dbo.Participants", "User_UserId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "User_UserId1", c => c.Int());
            AddColumn("dbo.Participants", "User_UserId", c => c.Int());
            AddColumn("dbo.Movies", "User_UserId1", c => c.Int());
            AddColumn("dbo.Movies", "User_UserId", c => c.Int());
            DropForeignKey("dbo.MoviesLikedBy", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.DirectorsLikedBy", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.DirectorsLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActorsLikedBy", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.ActorsLikedBy", "UserId", "dbo.Users");
            DropForeignKey("dbo.MoviesBoughtBy", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MoviesBoughtBy", "UserId", "dbo.Users");
            DropIndex("dbo.MoviesLikedBy", new[] { "MovieId" });
            DropIndex("dbo.MoviesLikedBy", new[] { "UserId" });
            DropIndex("dbo.DirectorsLikedBy", new[] { "ParticipantId" });
            DropIndex("dbo.DirectorsLikedBy", new[] { "UserId" });
            DropIndex("dbo.ActorsLikedBy", new[] { "ParticipantId" });
            DropIndex("dbo.ActorsLikedBy", new[] { "UserId" });
            DropIndex("dbo.MoviesBoughtBy", new[] { "MovieId" });
            DropIndex("dbo.MoviesBoughtBy", new[] { "UserId" });
            DropTable("dbo.MoviesLikedBy");
            DropTable("dbo.DirectorsLikedBy");
            DropTable("dbo.ActorsLikedBy");
            DropTable("dbo.MoviesBoughtBy");
            CreateIndex("dbo.Participants", "User_UserId1");
            CreateIndex("dbo.Participants", "User_UserId");
            CreateIndex("dbo.Movies", "User_UserId1");
            CreateIndex("dbo.Movies", "User_UserId");
            AddForeignKey("dbo.Movies", "User_UserId1", "dbo.Users", "UserId");
            AddForeignKey("dbo.Participants", "User_UserId1", "dbo.Users", "UserId");
            AddForeignKey("dbo.Participants", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Movies", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
