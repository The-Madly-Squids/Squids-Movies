namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenresToMovies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.Genres", new[] { "Movie_MovieId" });
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
            
            DropColumn("dbo.Genres", "Movie_MovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Movie_MovieId", c => c.Int());
            DropForeignKey("dbo.GenreMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropTable("dbo.GenreMovies");
            CreateIndex("dbo.Genres", "Movie_MovieId");
            AddForeignKey("dbo.Genres", "Movie_MovieId", "dbo.Movies", "MovieId");
        }
    }
}
