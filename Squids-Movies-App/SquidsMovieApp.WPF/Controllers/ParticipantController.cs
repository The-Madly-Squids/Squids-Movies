using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.DTO;
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

        public IEnumerable<ParticipantModel> FindParticipantsByNames(string pattern)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentNullException("Invalid search pattern!");
            }

            return this.participantService.SearchForParticipantsByNames(pattern);
        }

        public ParticipantModel GetParticipantByFirstAndLastNames(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException("Invalid participant first name!");
            }

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException("Invalid participant last name!");
            }
            
            return this.participantService.GetParticipantByNames(firstName, lastName);
        }

        public ParticipantModel GetParticipantById(int id)
        {           
            if (id < 1)
            {
                throw new ArgumentException("Invalid participant id!");
            }

            return this.participantService.GetParticipantById(id);
        }

        public IEnumerable<RoleModel> GetAllMoviesPerParticipantById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid participant id!");
            }
            
            return this.participantService.GetAllMoviesPerParticipant(id);
        }

        public IEnumerable<UserModel> GetParticipantFollowers(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid participant id!");
            }

            return this.participantService.GetParticipantFollowers(id);
        }
    }
}
