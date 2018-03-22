using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Utilities.Parsers.Models
{
    public class MovieParsedModel
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Plot")]
        public string Plot { get; set; }

        [JsonProperty("Year")]
        public int Year { get; set; }

        [JsonProperty("Rated")]
        public string Rated { get; set; }

        [JsonProperty("imdbRating")]
        public string ImdbRating { get; set; }

        [JsonProperty("Runtime")]
        public string Runtime { get; set; }

        [JsonProperty("Director")]
        public string Director { get; set; }

        [JsonProperty("Actors")]
        public string Actors { get; set; }

        [JsonProperty("Genre")]
        public string Genres { get; set; }
        
        [JsonProperty("Poster")]
        public string PosterUrl { get; set; }
    }
}
