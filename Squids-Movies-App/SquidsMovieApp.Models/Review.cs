using SquidsMovieApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Models
{
    public class Review
    {
        // a user can submit more then one review
        // must use a composite primary key
        public int ReviewId { get; set; }

        [StringLength(GlobalConstants.MaxReviewLength, MinimumLength = GlobalConstants.MinReviewLength)]
        public string Description { get; set; }

        [Range(GlobalConstants.MinReviewScore, GlobalConstants.MaxReviewScore)]
        public int Rating { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
