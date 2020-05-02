using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Solaris.Web.SolarApi.Core.Extensions
{
    public static class UriExtensions
    {
        public static async Task<bool> IsValidImageAsync(this Uri url)
        {
            var fileStream = GetStreamFromUrl(url);
            fileStream.Seek(0, SeekOrigin.Begin);
            try
            {
                Image.FromStream(fileStream);
                return true;
            }
            catch (ArgumentException)
            {
                return await IsValidWebPImageAsync(fileStream);
            }
            catch
            {
                await fileStream.DisposeAsync();
                return false;
            }
        }

        private static async Task<bool> IsValidWebPImageAsync(Stream fileStream)
        {
            //WebP Images can't be loaded in Image object so we must validate the image header to check if the image is valid
            const int bufferSize = 12;
            const string webPHeaderStart = "RIFF";
            const string webPHeaderEnd = "WEBP";

            fileStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(fileStream, Encoding.UTF8);
            var buffer = new char[bufferSize];
            await reader.ReadBlockAsync(buffer, 0, bufferSize);
            return new string(buffer.Take(webPHeaderStart.Length).ToArray()).Equals(webPHeaderStart) &&
                   new string(buffer.Skip(webPHeaderStart.Length).ToArray()).Contains(webPHeaderEnd);
        }

        private static Stream GetStreamFromUrl(Uri url)
        {
            byte[] imageData;

            using (var client = new WebClient())
            {
                imageData = client.DownloadData(url);
            }

            return new MemoryStream(imageData);
        }
    }
}