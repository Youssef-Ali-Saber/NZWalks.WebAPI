﻿namespace NZWalks.WebAPI.Models.Domain
{
	public class Region
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string? PhotoUrl { get; set; }
		//public ICollection<Walk> Walks { get; set; }

	}
}
