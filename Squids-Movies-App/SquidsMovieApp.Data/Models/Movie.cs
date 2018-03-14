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
        private string name;
        private string description;
        // rating needs rethinking
        private int rating;
        private int year;
        private int runningTime;

        public Movie()
        {

        }

        public Movie(string name, string description, int rating, int year,
            int runningTime)
        {
            this.Name = name;
            this.Description = description;
            this.Rating = rating;
            this.Year = year;
            this.RunningTime = runningTime;

            // db initialization - possible add more
            this.Participants = new HashSet<Participant>();
          

        }

        public virtual string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                Guard.WhenArgument(value, "movie name")
                    .IsNullOrEmpty()
                    .Throw();
                this.name = value;
            }
        }

        public virtual string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                Guard.WhenArgument(value, "description")
                    .IsNullOrEmpty()
                    .Throw();
                this.description = value;
            }
        }

        public virtual int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                Guard.WhenArgument(value, "year")
                    .IsLessThan(1850)
                    .IsGreaterThan(2020)
                    .Throw();
                this.year = value;
            }
        }

        public virtual int Rating
        {
            get
            {
                return this.Rating;
            }
            set
            {
                Guard.WhenArgument(value, "rating")
                    .IsLessThan(1)
                    .IsGreaterThan(10)
                    .Throw();
                this.rating = value;
            }
        }

        public int RunningTime
        {
            get
            {
                return this.runningTime;
            }
            set
            {
                Guard.WhenArgument(value, "movie runtime")
                    .IsLessThan(0)
                    .Throw();
                this.runningTime = value;
            }
        }

        // many-to-many
        public virtual ICollection<Participant> Participants { get; set; }
        public int MovieId { get; set; } // PK

    }
}
