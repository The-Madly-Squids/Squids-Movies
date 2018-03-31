using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.DTO
{
    public class MoviePosterModel
    {
        public int Id { get; set; }
        public byte[] Poster { get; set; }
        public virtual MovieModel Movie { get; set; }
    }
}
