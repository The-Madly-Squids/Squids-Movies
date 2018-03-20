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
       
        IEnumerable<ParticipantModel> GetLikedDirectors(UserModel user); //Done
        IEnumerable<ParticipantModel> GetLikedActors(UserModel user); //Done
        IEnumerable<MovieModel> GetLikedMovies(UserModel user); //Done
        IEnumerable<MovieModel> GetBoughtMovies(UserModel user); //Done
        //IEnumerable<ReviewModel> GetAllReviews();
        IEnumerable<UserModel> GetFollowers(UserModel user); //Done
        IEnumerable<UserModel> GetFollowed(UserModel user); //Done
        decimal GetMoneyBalance(UserModel user); //Done
        void AddMoneyToBalance(UserModel user, decimal amount); //Done
        void LikeActor(UserModel user, ParticipantModel actor); //Done
        void LikeDirector(UserModel user, ParticipantModel director); //Done
        void FollowUser(UserModel user, UserModel userToFollow); //Done
        void BuyMovie(UserModel user, MovieModel movie, decimal price); //Done
        void GiveReview(UserModel user, ReviewModel review, MovieModel movie); //Done
        // admin methods
        void AddUser(UserModel user); //Done
        void RemoveUser(UserModel user); //Done
        IEnumerable<UserModel> GetAllUsers(); //Done
    }
}
