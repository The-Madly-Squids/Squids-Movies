namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluentapiuserreviewmovie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "MovieId_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "UserId_UserId", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "MovieId_MovieId" });
            DropIndex("dbo.Reviews", new[] { "UserId_UserId" });
            RenameColumn(table: "dbo.Reviews", name: "MovieId_MovieId", newName: "Movie_MovieId");
            RenameColumn(table: "dbo.Reviews", name: "UserId_UserId", newName: "User_UserId");
            AlterColumn("dbo.Reviews", "Movie_MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reviews", "User_UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "Movie_MovieId");
            CreateIndex("dbo.Reviews", "User_UserId");
            AddForeignKey("dbo.Reviews", "Movie_MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.Reviews", new[] { "User_UserId" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieId" });
            AlterColumn("dbo.Reviews", "User_UserId", c => c.Int());
            AlterColumn("dbo.Reviews", "Movie_MovieId", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "User_UserId", newName: "UserId_UserId");
            RenameColumn(table: "dbo.Reviews", name: "Movie_MovieId", newName: "MovieId_MovieId");
            CreateIndex("dbo.Reviews", "UserId_UserId");
            CreateIndex("dbo.Reviews", "MovieId_MovieId");
            AddForeignKey("dbo.Reviews", "UserId_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Reviews", "MovieId_MovieId", "dbo.Movies", "MovieId");
        }
    }
}
