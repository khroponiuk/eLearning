using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eLearning.Core.Managers
{
    public class FileStorageManager
    {
        readonly string uploadFolderName = "upload";
        readonly string attachmentsFolderName = "attachments";
        readonly string storagePath = Path.Combine(Directory.GetCurrentDirectory(), "CLientApp", "public");
        readonly string uploadStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "CLientApp", "public", "upload");

        public string GetDefaultCourseImagePath()
        {
            return Path.Combine("images", "image-holder.png");
        }


        public void EnsureStorageCreated()
        {
            if (!Directory.Exists(uploadStoragePath))
                Directory.CreateDirectory(uploadStoragePath);

            var attachmentsFolder = Path.Combine(uploadStoragePath, attachmentsFolderName);

            if (!Directory.Exists(attachmentsFolder))
                Directory.CreateDirectory(attachmentsFolder);
        }

        public string SaveFile(IFormFile formFile)
        {
            if (formFile == null)
                return null;

            var fileName = Guid.NewGuid().ToString();
            var fileExtension = formFile.FileName.Substring(formFile.FileName.LastIndexOf("."));
            var fullFileName = fileName + fileExtension;

            var filePath = Path.Combine(uploadStoragePath, fullFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return Path.Combine(uploadFolderName, fullFileName);
        }

        protected void EnsureFolderExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void RemoveFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            try
            {
                var fullPath = Path.Combine(storagePath, filePath);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);

            }
            catch (IOException) { }
        }
    }
}
