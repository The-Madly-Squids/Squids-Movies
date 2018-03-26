using AutoMapper;
using SquidsMovieApp.Common.Mapping;
using SquidsMovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.DTO
{
    public class UserModel : IMapFrom<User>, IHaveCustomMapping
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public decimal MoneyBalance { get; set; }
        public ICollection<ParticipantModel> LikedParticipants { get; set; }
        public ICollection<MovieModel> LikedMovies { get; set; }
        public ICollection<MovieModel> BoughtMovies { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; }
        public ICollection<UserModel> Following { get; set; }
        public ICollection<UserModel> Followers { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
