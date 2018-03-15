using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
