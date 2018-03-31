using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Models;
using SquidsMovieApp.DTO;
using SquidsMovieApp.Logic.Contracts;

namespace SquidsMovieApp.Logic
{
    public class ParticipantService : IParticipantService
    {
        private readonly IMovieAppDBContext movieAppDbContext;
        private readonly IMapper mapper;

        public ParticipantService(IMovieAppDBContext movieAppDbContext, IMapper mapper)
        {
            this.movieAppDbContext = movieAppDbContext;
            this.mapper = mapper;
        }

        public void AddParticipant(ParticipantModel participantModel)
        {
            Guard.WhenArgument(participantModel, "participant model")
                .IsNull()
                .Throw();

            var participant = this.mapper.Map<Participant>(participantModel);

            this.movieAppDbContext.Participants.Add(participant);
        }

        public IEnumerable<ParticipantModel> FindParticipantsByNames(string pattern)
        {
            var participantsPoco = this.movieAppDbContext.Participants
                               .Where(x => x.FirstName.Contains(pattern) || x.LastName.Contains(pattern))
                               .ToList();

            var participantDto = mapper.Map<IList<ParticipantModel>>(participantsPoco);

            return participantDto;
        }

        public IEnumerable<MovieModel> GetAllMoviesPerParticipant(ParticipantModel participant)
        {
            throw new NotImplementedException();
        }

        public ParticipantModel GetParticipant(string firstName, string lastName)
        {
            var participant = this.movieAppDbContext.Participants.
                Where(x => x.FirstName == firstName && x.LastName == lastName)
                .FirstOrDefault();

            if (participant == null)
            {
                throw new ArgumentNullException("Participant not found!");
            }

            var participantDto = mapper.Map<ParticipantModel>(participant);

            return participantDto;
        }

        public decimal ParticipantRating(ParticipantModel participantModel)
        {
            throw new NotImplementedException();
        }

        public void RemoveParticipant(ParticipantModel participantModel)
        {
            throw new NotImplementedException();
        }
    }
}
