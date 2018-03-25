using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Data.Utilities.Parsers.Models
{
    public class ActorParsedModel
    {
        [JsonProperty("extract")]
        public string Bio { get; set; }
    }
}
