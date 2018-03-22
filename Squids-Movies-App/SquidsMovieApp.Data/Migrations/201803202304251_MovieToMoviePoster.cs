namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieToMoviePoster : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviePosters", "Id", "dbo.Movies");
            DropIndex("dbo.MoviePosters", new[] { "Id" });
            DropTable("dbo.MoviePosters");
        }
    }
}
