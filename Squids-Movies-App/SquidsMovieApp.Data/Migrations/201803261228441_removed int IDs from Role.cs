namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedintIDsfromRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.Roles", new[] { "ParticipantId" });
            DropIndex("dbo.Roles", new[] { "MovieId" });
            RenameColumn(table: "dbo.Roles", name: "MovieId", newName: "Movie_MovieId");
            RenameColumn(table: "dbo.Roles", name: "ParticipantId", newName: "Participant_ParticipantId");
            AlterColumn("dbo.Roles", "Participant_ParticipantId", c => c.Int());
            AlterColumn("dbo.Roles", "Movie_MovieId", c => c.Int());
            CreateIndex("dbo.Roles", "Movie_MovieId");
            CreateIndex("dbo.Roles", "Participant_ParticipantId");
            AddForeignKey("dbo.Roles", "Movie_MovieId", "dbo.Movies", "MovieId");
            AddForeignKey("dbo.Roles", "Participant_ParticipantId", "dbo.Participants", "ParticipantId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Roles", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.Roles", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Roles", new[] { "Movie_MovieId" });
            AlterColumn("dbo.Roles", "Movie_MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Roles", "Participant_ParticipantId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Roles", name: "Participant_ParticipantId", newName: "ParticipantId");
            RenameColumn(table: "dbo.Roles", name: "Movie_MovieId", newName: "MovieId");
            CreateIndex("dbo.Roles", "MovieId");
            CreateIndex("dbo.Roles", "ParticipantId");
            AddForeignKey("dbo.Roles", "ParticipantId", "dbo.Participants", "ParticipantId", cascadeDelete: true);
            AddForeignKey("dbo.Roles", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
        }
    }
}
