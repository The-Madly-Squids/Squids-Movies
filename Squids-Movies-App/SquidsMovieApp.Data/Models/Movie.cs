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
        }

        public int MovieId { get; set; } // PK
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Year { get; set; }
        public virtual int Rating { get; set; }
        public int RunningTime { get; set; }


        public virtual ICollection<User> LikedBy { get; set; }
        public virtual ICollection<User> BoughtBy { get; set; }

        // many-to-many
        public virtual ICollection<Participant> Participants { get; set; }


    }
}
