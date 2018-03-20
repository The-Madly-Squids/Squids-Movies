using Newtonsoft.Json;
using SquidsMovieApp.Utilities.Parsers.Models;
using System.Collections.Generic;
using System.IO;

namespace SquidsMovieApp.Utilities.Parsers
{
    public class Parser
    {
        public ICollection<MovieParsedModel> ParseMovies(string path)
        {
            return JsonConvert.DeserializeObject<List<MovieParsedModel>>(File.ReadAllText(path));
        }

        //public ICollection<PersonParser> ParsePersons(string path)
        //{
        //    return JsonConvert.DeserializeObject<List<PersonParser>>(File.ReadAllText(path));
        //}
    }
}
