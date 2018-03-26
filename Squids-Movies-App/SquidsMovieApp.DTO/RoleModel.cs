using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SquidsMovieApp.Common.Mapping;
using SquidsMovieApp.Data.Models;

namespace SquidsMovieApp.DTO
{
    public class RoleModel : IMapFrom<Role>, IHaveCustomMapping
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        //public int ParticipantId { get; set; }
        public virtual ParticipantModel Participant { get; set; }
        //public int MovieId { get; set; }
        public virtual MovieModel Movie { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
