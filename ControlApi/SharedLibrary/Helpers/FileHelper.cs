using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Helpers
{
    public static class FileHelper
    {
        public static string Upload(IFormFile File, IWebHostEnvironment Environment, string Name)
        {
            try
            {
                if (!Directory.Exists(Environment.WebRootPath + "\\Uploads\\"))
                {
                    Directory.CreateDirectory(Environment.WebRootPath + "\\Uploads\\");
                }

                string FilePath = "\\Uploads\\" + Name + Path.GetExtension(File.FileName);

                using (FileStream Stream = System.IO.File.Create(Environment.WebRootPath + FilePath))
                {
                    File.CopyTo(Stream);
                    Stream.Flush();
                    return FilePath;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
