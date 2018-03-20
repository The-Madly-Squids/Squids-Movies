namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkingonUser : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Reviews", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.Roles", new[] { "ParticipantId" });
            DropColumn("dbo.Reviews", "Rating");
            DropTable("dbo.Roles");
        }
    }
}
