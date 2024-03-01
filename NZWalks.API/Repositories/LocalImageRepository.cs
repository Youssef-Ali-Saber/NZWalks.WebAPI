using NZWalks.WebAPI.Data;
using NZWalks.WebAPI.Models.Domin;

namespace NZWalks.WebAPI.Repositories
{

	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly NZWalksDbContext _db;

		public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,NZWalksDbContext db)
        {
			_webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this._db = db;
		}
		public async Task<Image> Upload(Image image)
		{
			var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images",
				$"{image.FileName}{image.FileExtension}");
			using Stream fileStream = new FileStream(localPath, FileMode.Create);
			await image.File.CopyToAsync(fileStream);

			var urlfilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
			
			image.FilePath = urlfilePath;

			await _db.Images.AddAsync(image);
			await _db.SaveChangesAsync();

			return image;
		
		}
	}
}
