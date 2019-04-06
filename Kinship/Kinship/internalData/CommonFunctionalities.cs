using System;
using System.IO;

namespace Kinship.internalData
{
    public class CommonFunctionalities
    {
        //public static string baseAuthenticationUrl = "https://jsonplaceholder.typicode.com";

        public static string ImageToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        public static MemoryStream Base64ToImage(string base64Image)
        {
            byte[] Base64Stream = Convert.FromBase64String(base64Image);
            return new MemoryStream(Base64Stream);
        }
    }
}
