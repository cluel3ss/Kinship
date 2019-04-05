using System;
using System.IO;

namespace Kinship.internalData
{
    public class CommonFunctionalities
    {
        //public static string baseAuthenticationUrl = "https://jsonplaceholder.typicode.com";

        public static string ImageToBase64(Stream stream)
        {
            Byte[] bytes = File.ReadAllBytes("path");
            String file = Convert.ToBase64String(bytes);
            return file;
        }

        public static string Base64ToImage()
        {
            return "No Yet Implemented";
        }
    }
}
