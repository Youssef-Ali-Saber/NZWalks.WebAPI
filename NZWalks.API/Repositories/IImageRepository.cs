using NZWalks.WebAPI.Models.Domin;

namespace NZWalks.WebAPI.Repositories
{
	public interface IImageRepository
	{
		Task<Image> Upload(Image image);
	}
}
