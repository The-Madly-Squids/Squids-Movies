namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleAndMovieChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.Roles", new[] { "ParticipantId" });
            RenameColumn(table: "dbo.Roles", name: "ParticipantId", newName: "Participant_ParticipantId");
            AddColumn("dbo.Movies", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Movies", "ImdbRating", c => c.Double(nullable: false));
            AddColumn("dbo.Roles", "Movie_MovieId", c => c.Int());
            AlterColumn("dbo.Roles", "Participant_ParticipantId", c => c.Int());
            CreateIndex("dbo.Roles", "Movie_MovieId");
            CreateIndex("dbo.Roles", "Participant_ParticipantId");
            AddForeignKey("dbo.Roles", "Movie_MovieId", "dbo.Movies", "MovieId");
            AddForeignKey("dbo.Roles", "Participant_ParticipantId", "dbo.Participants", "ParticipantId");
            DropColumn("dbo.Roles", "MovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "MovieId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Roles", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Roles", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.Roles", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Roles", new[] { "Movie_MovieId" });
            AlterColumn("dbo.Roles", "Participant_ParticipantId", c => c.Int(nullable: false));
            DropColumn("dbo.Roles", "Movie_MovieId");
            DropColumn("dbo.Movies", "ImdbRating");
            DropColumn("dbo.Movies", "Price");
            RenameColumn(table: "dbo.Roles", name: "Participant_ParticipantId", newName: "ParticipantId");
            CreateIndex("dbo.Roles", "ParticipantId");
            AddForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants", "ParticipantId", cascadeDelete: true);
        }
    }
}
