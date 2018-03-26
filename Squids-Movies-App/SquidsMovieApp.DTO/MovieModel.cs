using AutoMapper;
using SquidsMovieApp.Common.Mapping;
using SquidsMovieApp.Models;
using System.Collections.Generic;

namespace SquidsMovieApp.DTO
{
    public class MovieModel : IMapFrom<Movie>, IHaveCustomMapping
    {
        public int MovieId { get; set; }
        public virtual string Title { get; set; }
        public string Plot { get; set; }
        public int? Year { get; set; }
        public int? Runtime { get; set; }
        public string Rated { get; set; }
        public double Price { get; set; }
        public double ImdbRating { get; set; }
        public MoviePosterModel Poster { get; set; }

        public virtual ICollection<ParticipantModel> Participants { get; set; }
        public virtual ICollection<UserModel> BoughtBy { get; set; }
        public virtual ICollection<UserModel> LikedBy { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Movie, MovieModel>()
            //    .ForMember(m => m.Name,
            //                cfg => cfg.MapFrom(m => m.Description + " " + m.Year + " "
            //                + m.RunningTime));
        }
    }
}

