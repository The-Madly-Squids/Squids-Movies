using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Core.Factories
{
    // must change factory name to IXxxModelFactory
    public class ParticipantModelFactory : IParticipantFactory
    {
        public ParticipantModel CreateParticipantModel(string firstName, string lastName, int age)
        {
            return new ParticipantModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };
        }
    }
}
