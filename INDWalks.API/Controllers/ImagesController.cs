using AutoMapper;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTOs;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImagesController(IMapper mapper, IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }
        //POST: https://localhost:portnumber/api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                // Conert DTO to Domain Model
                var image = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription= request.FileDescription,
                };

                //Use Repository to upload Image
                await imageRepository.Upload(image);

                return Ok(image);
            }

            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            
            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Type!!!");
            }

            if(request.File.Length > 10485760) //10 MB
            {
                ModelState.AddModelError("file", "File Size must be less or equal to 10 MB!!!");
            }
        }
    }
}
