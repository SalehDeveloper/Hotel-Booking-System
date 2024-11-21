using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Services
{
    public class ImageService
    {
        private readonly ImageHelper _imageHelper;

        public ImageService(IOptions<ImageHelper> imageHelper)
        {
            _imageHelper = imageHelper.Value;
        }

        public bool IsImageTypeAllowed(string extension)
        {
            return _imageHelper.AllowedTypes.Contains(extension.ToLower());
        }

        public string GetImageDirectory()
        {
            return _imageHelper.Directory;
        }

        public string GetDefaultImage()
        {
            return _imageHelper.DefaultPhoto;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            // Check if file is uploaded
            if (imageFile == null || imageFile.Length == 0)
            {
                return GetDefaultImage();      
            }

            // Validate file type
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();


            if (!IsImageTypeAllowed(fileExtension))
            {
                throw new ErrorResponseException(  400 ,"File type not allowed." , "Bad Request");
            }

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(GetImageDirectory(), fileName);

            // Ensure the directory exists
            if (!Directory.Exists(GetImageDirectory()))
            {
                Directory.CreateDirectory(GetImageDirectory());
            }



            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the relative file Name to save it in the Database 
            return fileName;
        }

        public string GetImagePath(string imageName)
        {
            var filePath = Path.Combine(GetImageDirectory(), imageName);

            return filePath;
        }
        public string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

        public  Image GetImage(string fileName)
        { 
            var image = new Image();    
            var filePath = Path.Combine(GetImageDirectory(), fileName);

            if (!System.IO.File.Exists(filePath))
                return null;

             image.MimeType = GetMimeType(filePath);
             image.File = System.IO.File.OpenRead(filePath);

            return image;
        }


    }
}
