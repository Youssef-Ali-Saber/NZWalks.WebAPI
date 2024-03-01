using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.WebAPI.Models.Domin;
using NZWalks.WebAPI.Models.DTO;
using NZWalks.WebAPI.Repositories;

namespace NZWalks.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository _imageRepository;

		public ImagesController(IImageRepository imageRepository)
        {
			this._imageRepository = imageRepository;
		}
        [HttpPost]
		public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto fileRequset)
		{
			validateFileUpload(fileRequset);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var image = new Image()
			{
				File = fileRequset.File,
				FileName = fileRequset.FileName,
				FileDescription = fileRequset.FileDescription,
				FileExtension = Path.GetExtension(fileRequset.File.FileName),
				FileSizeInBytes = fileRequset.File.Length
			};
			await _imageRepository.Upload(image);
			return Ok(image);
		}

		private void validateFileUpload(ImageUploadRequestDto fileRequset)
		{
			var allowExtensions = new List<string> { ".jpg", ".png", ".jpeg" };
			var maxFileSizeInBytes = 10485760;
			if (allowExtensions.Contains(fileRequset.File.FileName))
			{
				ModelState.AddModelError("file", "Invalid file extension");
			}
			if (fileRequset.File.Length > maxFileSizeInBytes)
			{
				ModelState.AddModelError("file", "File size is too large");
			}

		}
	}
}
