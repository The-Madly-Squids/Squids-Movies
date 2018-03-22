namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRolesFromParticipants : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Roles", name: "MovieId_MovieId", newName: "Movie_MovieId");
            RenameColumn(table: "dbo.Roles", name: "ParticipantId_ParticipantId", newName: "Participant_ParticipantId");
            RenameIndex(table: "dbo.Roles", name: "IX_MovieId_MovieId", newName: "IX_Movie_MovieId");
            RenameIndex(table: "dbo.Roles", name: "IX_ParticipantId_ParticipantId", newName: "IX_Participant_ParticipantId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Roles", name: "IX_Participant_ParticipantId", newName: "IX_ParticipantId_ParticipantId");
            RenameIndex(table: "dbo.Roles", name: "IX_Movie_MovieId", newName: "IX_MovieId_MovieId");
            RenameColumn(table: "dbo.Roles", name: "Participant_ParticipantId", newName: "ParticipantId_ParticipantId");
            RenameColumn(table: "dbo.Roles", name: "Movie_MovieId", newName: "MovieId_MovieId");
        }
    }
}
