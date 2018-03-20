using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Core.Factories.Contracts
{
    public interface IParticipantFactory
    {

        ParticipantModel CreateParticipantModel(string firstName, string lastName,
            int age);
    }
}
