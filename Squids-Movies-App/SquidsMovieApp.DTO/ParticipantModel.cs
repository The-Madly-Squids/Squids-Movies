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
    public class ParticipantModel : IMapFrom<Participant>, IHaveCustomMapping
    {

        public int ParticipantId { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<User> ActorLikedBy { get; set; }
        public virtual ICollection<User> DirectorLikedBy { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
