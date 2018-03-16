using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;

namespace SquidsMovieApp.Data.Models
{
    public class Movie
    {

        public Movie()
        {
            this.Participants = new HashSet<Participant>();
            this.LikedBy = new HashSet<User>();
            this.BoughtBy = new HashSet<User>();
            this.Reviews = new HashSet<Review>();
            this.Genres = new HashSet<Genre>();
        }

        public int MovieId { get; set; } // PK

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public virtual string Name { get; set; }

        [StringLength(200, MinimumLength = 10)]
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? Rating { get; set; }
        public int? RunningTime { get; set; }


        public virtual ICollection<User> LikedBy { get; set; }
        public virtual ICollection<User> BoughtBy { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        // many-to-many
        public virtual ICollection<Participant> Participants { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}
