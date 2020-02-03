using System.Drawing;
using System.IO;


namespace Workwise.Helper
{
    public static class Base64Helper
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static Image Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            Image image;
            using (MemoryStream ms = new MemoryStream(base64EncodedBytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

    }
}