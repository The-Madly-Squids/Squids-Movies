namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedreviewtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Description", c => c.String());
            AddColumn("dbo.Reviews", "MovieId_MovieId", c => c.Int());
            AddColumn("dbo.Reviews", "UserId_UserId", c => c.Int());
            CreateIndex("dbo.Reviews", "MovieId_MovieId");
            CreateIndex("dbo.Reviews", "UserId_UserId");
            AddForeignKey("dbo.Reviews", "MovieId_MovieId", "dbo.Movies", "MovieId");
            AddForeignKey("dbo.Reviews", "UserId_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId_UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "MovieId_MovieId", "dbo.Movies");
            DropIndex("dbo.Reviews", new[] { "UserId_UserId" });
            DropIndex("dbo.Reviews", new[] { "MovieId_MovieId" });
            DropColumn("dbo.Reviews", "UserId_UserId");
            DropColumn("dbo.Reviews", "MovieId_MovieId");
            DropColumn("dbo.Reviews", "Description");
        }
    }
}
