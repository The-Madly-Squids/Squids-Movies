using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.Data.Models;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IMovieService
    {
        IEnumerable<MovieModel> GetAllMovies();
        void AddMovie(MovieModel movie);
        void RemoveMovie(MovieModel movie);
        IEnumerable<ParticipantModel> GetAllParticipantsPerMovie(MovieModel movie);
        void AddMovieParticipant(MovieModel movie, ParticipantModel participant,
            string roleName);
        double GetRating(MovieModel movie);
        //IEnumerable<ReviewModel> GetReviews(MovieModel movie);
        IEnumerable<ParticipantModel> GetActors(MovieModel movie);
        IEnumerable<ParticipantModel> GetDirectors(MovieModel movie);
        IEnumerable<string> GetMovieGenres(MovieModel movie);
        IEnumerable<UserModel> GetUsersWhoBoughtIt(MovieModel movie);
        IEnumerable<UserModel> GetUsersWhoLikedtIt(MovieModel movie);
    }
}
