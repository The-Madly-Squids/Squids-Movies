using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bytes2you.Validation;
using SquidsMovieApp.Core.Factories.Contracts;
using SquidsMovieApp.Data.Context;
using SquidsMovieApp.Data.Models;
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
    }
}
