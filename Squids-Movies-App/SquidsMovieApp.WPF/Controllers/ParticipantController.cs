using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.WPF.Controllers
{
    public class ParticipantController
    {
        private readonly IMapper mapper;
        private readonly IParticipantService participantService;
        private readonly IParticipantFactory factory;

        public ParticipantController(IMapper mapper, IParticipantService participantService,
            IParticipantFactory factory)
        {
            this.mapper = mapper;
            this.participantService = participantService;
            this.factory = factory;

        }

        public void AddParticipant(string firstName, string lastName, int age)
        {
            Guard.WhenArgument(firstName, "participant first name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(lastName, "participant last name")
                .IsNullOrEmpty()
                .Throw();

            Guard.WhenArgument(age, "participant age")
                .IsLessThan(0)
                .IsGreaterThan(1200) // Yoda jedi was 900, so 1200 is good :)
                .Throw();

            var participantModel = this.factory.CreateParticipantModel(
                firstName, lastName, age);
            this.participantService.AddParticipant(participantModel);
        }
    }
}
