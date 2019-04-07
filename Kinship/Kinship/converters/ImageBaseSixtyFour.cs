using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Kinship.converters
{
    public class ImageBaseSixtyFour : IValueConverter
    {
        public ImageBaseSixtyFour() {}
        //public object Convert(object value, Type targetType, object parameter, string language)
        //{
        //    byte[] Base64Stream = System.Convert.FromBase64String(value.ToString());
        //    return new MemoryStream(Base64Stream);
        //    //throw new NotImplementedException();
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] Base64Stream = System.Convert.FromBase64String(value.ToString());
            return new MemoryStream(Base64Stream);
        }

        //public object ConvertBack(object value, Type targetType, object parameter, string language)
        //{
        //    //throw new NotImplementedException();
        //    byte[] bytes;
        //    Stream stream = new MemoryStream(value.ToString());
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        stream.CopyTo(memoryStream);
        //        bytes = memoryStream.ToArray();
        //    }

        //    string base64 = System.Convert.ToBase64String(bytes);

        //    return base64;
        //}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
