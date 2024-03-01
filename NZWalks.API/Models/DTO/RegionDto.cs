using NZWalks.WebAPI.Models.Domain;

namespace NZWalks.WebAPI.Models.DTO
{
	public class RegionDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string? PhotoUrl { get; set; }
		//public ICollection<Walk> Walks { get; set; }
	}
}
