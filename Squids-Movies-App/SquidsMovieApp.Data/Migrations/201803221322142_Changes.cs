namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "MoneyBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "MoneyBalance", c => c.Int(nullable: false));
        }
    }
}
