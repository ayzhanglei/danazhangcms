using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DanaZhangCms.Services
{
    public interface IMediaService
    {
       

        void SaveMedia(Stream mediaBinaryStream, string fileName, string mimeType = null);

        string GetMediaUrl(string fileName);

    }
}
