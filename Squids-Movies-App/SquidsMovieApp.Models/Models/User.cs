using SquidsMovieApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class User
    {
        public User()
        {
            this.LikedParticipants = new HashSet<Participant>();
            this.LikedMovies = new HashSet<Movie>();
            this.BoughtMovies = new HashSet<Movie>();
            this.Following = new HashSet<User>();
            this.Followers = new HashSet<User>();
            this.Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }

        [StringLength(GlobalConstants.MaxUserFirstNameLength, MinimumLength = GlobalConstants.MinUserFirstNameLength)]
        public string FirstName { get; set; }
        
        [StringLength(GlobalConstants.MaxUserLastNameLength, MinimumLength = GlobalConstants.MinUserLastNameLength)]
        public string LastName { get; set; }

        [Range(GlobalConstants.MinUserAge,GlobalConstants.MaxUserAge)]
        public int? Age { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxUserUsernameLength, MinimumLength = GlobalConstants.MinUserUsernameLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinUserPasswordLength)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public decimal MoneyBalance { get; set; }
        
        public virtual ICollection<Participant> LikedParticipants { get; set; }

        public virtual ICollection<Movie> LikedMovies { get; set; }
        public virtual ICollection<Movie> BoughtMovies { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<User> Following { get; set; }
        public virtual ICollection<User> Followers { get; set; }

    }
}
