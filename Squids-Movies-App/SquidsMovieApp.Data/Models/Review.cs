using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [StringLength(200, MinimumLength = 10)]
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
