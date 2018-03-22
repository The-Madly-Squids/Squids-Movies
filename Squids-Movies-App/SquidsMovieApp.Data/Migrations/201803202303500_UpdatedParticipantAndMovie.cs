namespace SquidsMovieApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedParticipantAndMovie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParticipantMovies", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.ParticipantMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Movies", "Poster_Id", "dbo.MoviePosters");
            DropIndex("dbo.Movies", new[] { "Poster_Id" });
            DropIndex("dbo.ParticipantMovies", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.ParticipantMovies", new[] { "Movie_MovieId" });
            AddColumn("dbo.Participants", "Bio", c => c.String(maxLength: 250));
            DropColumn("dbo.Movies", "Poster_Id");
            DropTable("dbo.MoviePosters");
            DropTable("dbo.ParticipantMovies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ParticipantMovies",
                c => new
                    {
                        Participant_ParticipantId = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_ParticipantId, t.Movie_MovieId });
            
            CreateTable(
                "dbo.MoviePosters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        Poster = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "Poster_Id", c => c.Int());
            DropColumn("dbo.Participants", "Bio");
            CreateIndex("dbo.ParticipantMovies", "Movie_MovieId");
            CreateIndex("dbo.ParticipantMovies", "Participant_ParticipantId");
            CreateIndex("dbo.Movies", "Poster_Id");
            AddForeignKey("dbo.Movies", "Poster_Id", "dbo.MoviePosters", "Id");
            AddForeignKey("dbo.ParticipantMovies", "Movie_MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.ParticipantMovies", "Participant_ParticipantId", "dbo.Participants", "ParticipantId", cascadeDelete: true);
        }
    }
}
