namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoviesTableAddedRatedAndPoster : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreMovies", newName: "MovieGenres");
            DropPrimaryKey("dbo.MovieGenres");
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        ParticipantId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.ParticipantId);
            
            CreateTable(
                "dbo.MoviePosters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        Poster = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Movies", "Plot", c => c.String(maxLength: 200));
            AddColumn("dbo.Movies", "Runtime", c => c.Int());
            AddColumn("dbo.Movies", "Rated", c => c.String());
            AddColumn("dbo.Movies", "Poster_Id", c => c.Int());
            AddColumn("dbo.Reviews", "Rating", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MovieGenres", new[] { "Movie_MovieId", "Genre_Id" });
            CreateIndex("dbo.Movies", "Poster_Id");
            AddForeignKey("dbo.Movies", "Poster_Id", "dbo.MoviePosters", "Id");
            DropColumn("dbo.Movies", "Name");
            DropColumn("dbo.Movies", "Description");
            DropColumn("dbo.Movies", "Rating");
            DropColumn("dbo.Movies", "RunningTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "RunningTime", c => c.Int());
            AddColumn("dbo.Movies", "Rating", c => c.Int());
            AddColumn("dbo.Movies", "Description", c => c.String(maxLength: 200));
            AddColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.Movies", "Poster_Id", "dbo.MoviePosters");
            DropForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.Roles", new[] { "ParticipantId" });
            DropIndex("dbo.Movies", new[] { "Poster_Id" });
            DropPrimaryKey("dbo.MovieGenres");
            DropColumn("dbo.Reviews", "Rating");
            DropColumn("dbo.Movies", "Poster_Id");
            DropColumn("dbo.Movies", "Rated");
            DropColumn("dbo.Movies", "Runtime");
            DropColumn("dbo.Movies", "Plot");
            DropColumn("dbo.Movies", "Title");
            DropTable("dbo.MoviePosters");
            DropTable("dbo.Roles");
            AddPrimaryKey("dbo.MovieGenres", new[] { "Genre_Id", "Movie_MovieId" });
            RenameTable(name: "dbo.MovieGenres", newName: "GenreMovies");
        }
    }
}
