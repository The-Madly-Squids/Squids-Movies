using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    // one-to-many
    public class Review
    {
        public int ReviewId { get; set; }
        public string Description { get; set; }
        public User UserId;
        public Movie MovieId;

    }
}
