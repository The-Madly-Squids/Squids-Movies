using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        }

        public int MovieId { get; set; } // PK
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Year { get; set; }
        public virtual int Rating { get; set; }
        public int RunningTime { get; set; }
        // many-to-many
        public virtual ICollection<Participant> Participants { get; set; }


    }
}
