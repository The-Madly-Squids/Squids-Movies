using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.Utilities.Converters
{
    public static class ImageConverter
    {
        public static string GetFromUrl(string url, string fileName)
        {
            string path = $@"..\..\..\{fileName}.jpg";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), path);
            }

            return path;
        }

        public static byte[] ConvertImageToByteArray(string ImagePath)
        {
            byte[] b = File.ReadAllBytes(ImagePath);

            return b;
        }
    }
}
