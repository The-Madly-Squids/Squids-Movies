namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedLengthRestrictions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Participants", "Bio", c => c.String(maxLength: 500));
            AlterColumn("dbo.MoviePosters", "Poster", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MoviePosters", "Poster", c => c.Binary());
            AlterColumn("dbo.Participants", "Bio", c => c.String(maxLength: 250));
        }
    }
}
