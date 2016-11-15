using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Services
{
    public class LocalMediaService : IMediaService
    {
        private const string MediaRootFoler = "Uploads";

    
       

        public void SaveMedia(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
           

            var filePath = Path.Combine(GlobalConfiguration.ContentPath, MediaRootFoler, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                mediaBinaryStream.CopyTo(output);
            }
        }

        public string GetMediaUrl(string fileName)
        {
            return $"/{MediaRootFoler}/{fileName}";
        }


    }
}
