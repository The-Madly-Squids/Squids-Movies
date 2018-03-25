namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedNicknameColumnAndRemovedRequiredFirstAndLastNamesRestrictions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 30));
            DropColumn("dbo.Users", "Nickname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Nickname", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Users", "Username");
        }
    }
}
