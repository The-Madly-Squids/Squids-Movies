using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IParticipantService
    {
        void AddParticipant(ParticipantModel participantModel);
        void RemoveParticipant(ParticipantModel participantModel);
        decimal ParticipantRating(ParticipantModel participantModel);
        IEnumerable<MovieModel> GetAllMoviesPerParticipant(ParticipantModel participant);
    }
}
