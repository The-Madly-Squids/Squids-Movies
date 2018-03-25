using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Common.Constants
{
    public static class GlobalConstants
    {
        // User
        public const int MaxUserFirstNameLength = 30;
        public const int MinUserFirstNameLength = 2;

        public const int MaxUserLastNameLength = 30;
        public const int MinUserLastNameLength = 2;

        public const int MaxUserUsernameLength = 30;
        public const int MinUserUsernameLength = 2;

        public const int MaxUserAge = 120;
        public const int MinUserAge = 0;

        public const int MinUserPasswordLength = 5;

        // Review
        public const int MaxReviewLength = 200;
        public const int MinReviewLength = 10;

        public const int MaxReviewScore = 10;
        public const int MinReviewScore = 0;

        //Participant
        public const int MaxParticipantFirstNameLength = 30;
        public const int MinParticipantFirstNameLength = 2;

        public const int MaxParticipantLastNameLength = 30;
        public const int MinParticipantLastNameLength = 2;

        public const int MaxParticipantAge = 120;
        public const int MinParticipantAge = 0;

        public const int MaxParticipantBioLength = 500;
        public const int MinParticipantBioLength = 10;

        // Movie
        public const int MaxMovieTitleLength = 100;
        public const int MinMovieTitleLength = 1;

        public const int MaxMoviePlotLength = 1000;
        public const int MinMoviePlotLength = 1;

        public const double MinMoviePrice = 0;
        public const double MaxMoviePrice = double.MaxValue;
    }
}
