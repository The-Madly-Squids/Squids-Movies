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
            this.LikedActors = new HashSet<Participant>();
            this.LikedDirectors = new HashSet<Participant>();
            this.LikedMovies = new HashSet<Movie>();
            this.BoughtMovies = new HashSet<Movie>();
            this.Following = new HashSet<User>();
            this.Followers = new HashSet<User>();
            this.Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public decimal MoneyBalance { get; set; }

        public virtual ICollection<Participant> LikedActors { get; set; }
        public virtual ICollection<Participant> LikedDirectors { get; set; }

        public virtual ICollection<Movie> LikedMovies { get; set; }
        public virtual ICollection<Movie> BoughtMovies { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<User> Following { get; set; }
        public virtual ICollection<User> Followers { get; set; }

    }
}
