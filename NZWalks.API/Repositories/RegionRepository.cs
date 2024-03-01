using Microsoft.EntityFrameworkCore;
using NZWalks.WebAPI.Data;
using NZWalks.WebAPI.Models.Domain;

namespace NZWalks.WebAPI.Repositories
{
	public class RegionRepository: IRegionRepository
	{
		private readonly NZWalksDbContext _db;
		public RegionRepository(NZWalksDbContext db)
		{
			_db = db;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			await _db.Regions.AddAsync(region);
			await _db.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var regionDomin = await _db.Regions.FindAsync(id);
			if (regionDomin == null)
			{
				return null;
			}
			_db.Regions.Remove(regionDomin);
			await _db.SaveChangesAsync();
			return regionDomin;
		}

		public async Task<IEnumerable<Region>> GetAllAsync()
		{
			return await _db.Regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			return await _db.Regions.FindAsync(id);
		}

		public async Task<Region?> UpdateAsync(Guid id, Region region)
		{
			var regionDomin = await _db.Regions.FindAsync(id);
			if (regionDomin == null)
			{
				return null;
			}
			regionDomin.Name = region.Name;
			regionDomin.Code = region.Code;
			regionDomin.PhotoUrl = region.PhotoUrl;
			await _db.SaveChangesAsync();
			return regionDomin;
		}
	}
}
