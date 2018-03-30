using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.DTO.Contracts
{
    public interface IParticipantModel
    {
        int ParticipantId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int? Age { get; set; }
        ICollection<MovieModel> Movies { get; set; }
        ICollection<UserModel> ParticipantLikedByUser { get; set; }
        ICollection<RoleModel> Roles { get; set; }

        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
