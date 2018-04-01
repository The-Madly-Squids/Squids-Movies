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
        ParticipantModel GetParticipantByNames(string firstName, string lastName);
        IEnumerable<ParticipantModel> SearchForParticipantsByNames(string pattern);
        ParticipantModel GetParticipantById(int id);
        IEnumerable<RoleModel> GetAllMoviesPerParticipant(int id);
        IEnumerable<UserModel> GetParticipantFollowers(int id);
    }
}
