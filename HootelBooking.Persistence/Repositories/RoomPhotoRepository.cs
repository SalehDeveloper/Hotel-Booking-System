using HootelBooking.Application.Contracts;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Repositories
{
    public class RoomPhotoRepository : BaseRepository<RoomPhoto>, IRoomPhotoRepository
    {   
        private readonly ImageService _imageService;
        public RoomPhotoRepository(AppDbContext context  , ImageService imageService) : base(context)
        {
            _imageService = imageService;   
        }

        public async Task<bool> DeleteAsync(List<string> photoNames)
        {
            var photos = _context.RoomPhotos
                         .Where(r => photoNames.Contains(r.PhotoName))
                         .ToList();

            if (photos == null || photos.Count == 0)
                return false;
            var missingPhotos = photoNames.Except(photos.Select(p => p.PhotoName)).ToList();
            if (missingPhotos.Any())
            {
                return false;
            }
            _context.RoomPhotos.RemoveRange(photos);

            foreach (var photo in photos)
            {
                var filePath = Path.Combine(_imageService.GetImageDirectory(), photo.PhotoName);

                if (File.Exists(filePath))
                    
                        File.Delete(filePath); // Delete the file from disk
                                    
            }
            await _context.SaveChangesAsync();

            return true; 

               
            
            
        }
    }
}
