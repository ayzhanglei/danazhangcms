using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using DanaZhangCms.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DanaZhangCms.Areas.SysAdmin.Controllers
{
    [Area("SysAdmin")]
    [Authorize(Roles = "admin")]
    public class ToolsController : Controller
    {
        private readonly IMediaService mediaService;
        public ToolsController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }
        [HttpPost]
        public IActionResult Upload()
        {
            IFormFile photo = Request.Form.Files[0];
            var url = SaveFile(photo);
            return Json(new { url = mediaService.GetMediaUrl(url), success = 1, message = "" });
        }
        private string SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            mediaService.SaveMedia(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
    }
}
