namespace NZWalks.WebAPI.Models.DTO
{
	public class WalkDto
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public double LengthInKm { get; set; }
		public String? WalkPhotoUrl { get; set; }
		public RegionDto Region { get; set; }
		public DifficultyDto Difficulty { get; set; }
	}
}
