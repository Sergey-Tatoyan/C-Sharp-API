using ArpaMedia.Web.Api.Provider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.UploadServices
{
    public class ImageProvider
    {
        /// <summary>
        /// Upload image and save it in directory.
        /// </summary>
        /// <param name="imageStream">Data of the image.</param> 
        /// <returns name="String">Path of newly created image.</returns> 
        public string UploadImage(Stream imageStream)
        {
            var memoryStream = new MemoryStream();
            imageStream.CopyToAsync(memoryStream);
            if (memoryStream.Length == 0)
            {
                throw new ArgumentException("Image can not be null.");
            }

            Image image = Image.FromStream(memoryStream);
            if (image == null)
            {
                throw new ArgumentException("File is not correct image.");
            }

            if (!image.RawFormat.Equals(ImageFormat.Jpeg) && !image.RawFormat.Equals(ImageFormat.Png))
            {
                throw new ArgumentException("Image format is not supported.");
            }

            if (memoryStream.Length > 5242880)
            {
                throw new ArgumentException("Image is to big.");
            }

            string fileName = GeneralProvider.GenerateRundomString();
            string imageSaveDirectory = @"Resources\Images";
            fileName = fileName + "." + image.RawFormat.ToString().ToLower();
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), imageSaveDirectory);
            var fullPath = Path.Combine(pathToSave, fileName);

            FileStream fileStream = new FileStream(fullPath, FileMode.Create);
            memoryStream.WriteTo(fileStream);
            fileStream.Close();
            memoryStream.Close();
            return imageSaveDirectory + @"\" + fileName;
        }

        /// <summary>
        /// Delete uploaded file.
        /// </summary>
        /// <param name="imagePath">The path of the file to be deleted.</param> 
        public  void DeleteFile(string imagePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
