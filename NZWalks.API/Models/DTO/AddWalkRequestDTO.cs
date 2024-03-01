using System.ComponentModel.DataAnnotations;

namespace NZWalks.WebAPI.Models.DTO
{
	public class AddWalkRequestDTO
	{
		[Required]
		[MaxLength(100)]
		public String Name { get; set; }
		[Required]
		[MaxLength(1000)]
		public String Description { get; set; }
		[Required]
		[Range(0, 100)]
		public double LengthInKm { get; set; }
		public String? WalkPhotoUrl { get; set; }
		[Required]
		public Guid RegionId { get; set; }
		[Required]
		public Guid DifficultyId { get; set; }

	}
}
