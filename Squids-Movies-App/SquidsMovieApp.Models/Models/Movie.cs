using SquidsMovieApp.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public int MovieId { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxMovieTitleLength, MinimumLength = GlobalConstants.MinMovieTitleLength)]
        public virtual string Title { get; set; }

        [StringLength(GlobalConstants.MaxMoviePlotLength, MinimumLength = GlobalConstants.MinMoviePlotLength)]
        public string Plot { get; set; }

        public int? Year { get; set; }
        public int? Runtime { get; set; }
        public string Rated { get; set; }

        [Range(GlobalConstants.MinMoviePrice, GlobalConstants.MaxMoviePrice)]
        public double Price { get; set; }

        public double ImdbRating { get; set; }

        public virtual MoviePoster Poster { get; set; }

        public virtual ICollection<User> LikedBy { get; set; }
        public virtual ICollection<User> BoughtBy { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

    }
}
