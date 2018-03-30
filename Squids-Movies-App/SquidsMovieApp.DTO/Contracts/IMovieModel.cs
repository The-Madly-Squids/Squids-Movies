using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.DTO.Contracts
{
    public interface IMovieModel
    {
        string Title { get; set; }
        string Plot { get; set; }
        int? Year { get; set; }
        int? Runtime { get; set; }
        int MovieId { get; set; }
        string Rated { get; set; }
        double Price { get; set; }
        double ImdbRating { get; set; }
        MoviePosterModel Poster { get; set; }

        ICollection<ParticipantModel> Participants { get; set; }
        ICollection<UserModel> BoughtBy { get; set; }
        ICollection<UserModel> LikedBy { get; set; }

        void CreateMappings(IMapperConfigurationExpression configuration);


    }
}
