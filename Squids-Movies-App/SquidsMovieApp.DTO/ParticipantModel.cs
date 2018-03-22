﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SquidsMovieApp.Common.Mapping;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.DTO
{
    public class ParticipantModel : IMapFrom<Participant>, IHaveCustomMapping
    {

        public int ParticipantId { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public virtual ICollection<MovieModel> Movies { get; set; }
        public virtual ICollection<UserModel> ActorLikedBy { get; set; }
        public virtual ICollection<UserModel> DirectorLikedBy { get; set; }
        public virtual ICollection<RoleModel> Roles { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
