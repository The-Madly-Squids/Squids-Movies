namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedMovieRestrictions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Movies", "Plot", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Plot", c => c.String(maxLength: 200));
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
