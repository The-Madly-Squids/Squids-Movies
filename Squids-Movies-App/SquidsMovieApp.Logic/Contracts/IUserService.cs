using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquidsMovieApp.DTO;

namespace SquidsMovieApp.Logic.Contracts
{
    public interface IUserService
    {
        // possible GetLikedActors/Directors ?
        IEnumerable<ParticipantModel> GetLikedParticipants();
        IEnumerable<MovieModel> GetLikeddMovies();
        IEnumerable<MovieModel> GetBoughtMovies();
        //IEnumerable<ReviewModel> GetAllReviews();
        IEnumerable<UserModel> GetFollowers();
        IEnumerable<UserModel> GetFollowed();
        decimal GetMoneyBalance();
        void AddMoneyToBalance(decimal amount);
        void LikeParticipant(ParticipantModel participant);
        void FollowUser(UserModel user);
        void BuyMovie(MovieModel movie);
        void GiveReview(MovieModel movie);
        // admin methods
        void AddUser(UserModel user);
        void RemoveUser(UserModel user);
        IEnumerable<UserModel> GetAllUsers();
    }
}
