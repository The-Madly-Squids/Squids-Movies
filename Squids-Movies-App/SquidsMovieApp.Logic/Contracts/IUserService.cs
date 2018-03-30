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

        IEnumerable<ParticipantModel> GetLikedParticipants(UserModel user); //Done
        IEnumerable<MovieModel> GetLikedMovies(UserModel user); //Done
        IEnumerable<MovieModel> GetBoughtMovies(UserModel user); //Done
        //IEnumerable<ReviewModel> GetAllReviews();
        IEnumerable<UserModel> GetFollowers(UserModel user); //Done
        IEnumerable<UserModel> GetFollowed(UserModel user); //Done
        decimal GetMoneyBalance(UserModel user); //Done
        void AddMoneyToBalance(UserModel user, decimal amount); //Done
        void LikeParticipant(UserModel user, ParticipantModel participant); //Done
        void FollowUser(UserModel user, UserModel userToFollow); //Done
        void BuyMovie(UserModel user, MovieModel movie, decimal price); //Done
        void GiveReview(UserModel user, MovieModel movie, int reviewRating,
                            string reviewDescription); //Done
        void EditUserFirstName(UserModel currentUser, string newName);
        void EditUserLastName(UserModel currentUser, string newName);

        // admin methods
        void AddUser(UserModel user); //Done
        //void RemoveUser(UserModel user); //Done
        void RemoveUser(UserModel user); //Done
        IEnumerable<UserModel> GetAllUsers(); //Done
        UserModel GetUserByUsername(string userName); // Done
        UserModel GetUserByEmail(string email);
    }
}
