using System.ComponentModel.DataAnnotations;

namespace NZWalks.WebAPI.Models.DTO
{
	public class UpdateRegionRequestDTO
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[MaxLength(3)]
		[MinLength(3)]
		public string Code { get; set; }
		public string? PhotoUrl { get; set; }
	}
}
