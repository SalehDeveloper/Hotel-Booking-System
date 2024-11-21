
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HootelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        
      

        private readonly ImageService _imageService;
        public ImageController(IMediator mediator , ImageService imageService)
        {
         
            _imageService = imageService;
        }

        [HttpGet]
        [Route("GetImage/{fileName}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> GetImage([FromRoute] string fileName)
        {
            var res = _imageService.GetImage(fileName);

            if (res != null && res.File != null)
                return File(res.File, res.MimeType);

            return NotFound();

        }
    } 
}
