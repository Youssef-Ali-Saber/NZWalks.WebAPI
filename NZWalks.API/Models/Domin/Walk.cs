namespace NZWalks.API.Models.Domin
{
	public class Walk   
	{
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double LengthInKm { get; set; }
		public String? WalkPhotoUrl { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; } 
}
}
