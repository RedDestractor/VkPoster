using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VkPoster.Helpers
{
    class DataContentHelper
    {
        public static Stream DownloadImage(Uri uri, string savePath)
        {
            var request = WebRequest.Create(uri);
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                Byte[] buffer = new Byte[response.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                }
                while (actuallyRead > 0);
                File.WriteAllBytes(savePath, buffer);
                return new MemoryStream(buffer);
            }
        }
    }
}
