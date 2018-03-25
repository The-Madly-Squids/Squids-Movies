using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SquidsMovieApp.Data.Utilities.Converters
{
    /// <summary>
    /// Gets a resource from an url and converts it to a c# object.
    /// </summary>
    public class WebConverter
    {
        public byte[] ImageFromUrlToByreArray(string url)
        {
            byte[] byteArr;
            
            using (WebClient client = new WebClient())
            {
                byteArr = client.DownloadData(url);
            }

            return byteArr;
        }

        public string JsonFromUrlToString(string url)
        {
            string str;

            using (WebClient client = new WebClient())
            {
                str = client.DownloadString(url);
            }

            return str;
        }
    }
}
