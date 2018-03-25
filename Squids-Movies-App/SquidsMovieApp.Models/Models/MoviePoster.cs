using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class MoviePoster
    {
        [ForeignKey("Movie")]
        public int Id { get; set; }

        [Required]
        public byte[] Poster { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
