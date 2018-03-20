using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Models
{
    public class MoviePoster
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public byte[] Poster { get; set; }
    }
}
