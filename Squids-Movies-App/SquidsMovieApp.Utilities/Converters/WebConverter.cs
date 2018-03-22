using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SquidsMovieApp.Utilities.Converters
{
    /// <summary>
    /// Gets a resource from an url and converts it to a c# object.
    /// </summary>
    public class WebConverter
    {
        //public static string GetImageFromUrl(string url, string fileName)
        //{
        //    var newName = fileName.Replace(':', ' ');
        //    string path = $@"{newName}.jpg";

        //    using (WebClient client = new WebClient())
        //    {
        //        client.DownloadFile(new Uri(url), path);
        //    }

        //    return path;
        //}

        //public static byte[] FromImageToByteArray(string ImagePath)
        //{
        //    byte[] byteArr = File.ReadAllBytes(ImagePath);

        //    return byteArr;
        //}

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
