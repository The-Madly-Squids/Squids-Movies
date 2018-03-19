using AutoMapper;
using SquidsMovieApp.Common.Mapping;
using SquidsMovieApp.Data.Models;
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
        public int MoneyBalance { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
