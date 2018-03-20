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
    public class MovieModel : IMapFrom<Movie>, IHaveCustomMapping
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? RunningTime { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<User> BoughtBy { get; set; }
        public virtual ICollection<User> LikedBy { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Movie, MovieModel>()
            //    .ForMember(m => m.Name,
            //                cfg => cfg.MapFrom(m => m.Description + " " + m.Year + " "
            //                + m.RunningTime));
        }
    }
}

