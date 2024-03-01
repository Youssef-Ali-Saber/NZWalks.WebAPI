using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks.WebAPI.Data;
using NZWalks.WebAPI.Models.Domain;
using NZWalks.WebAPI.Repositories;

namespace NZWalks.WebAPI.Repositories
{
	public class WalkRepository : IWalkRepository
	{
		private readonly NZWalksDbContext _db;
		public WalkRepository(NZWalksDbContext db)
		{
			_db = db;
		}
		public async Task<IEnumerable<Walk>> GetAllAsync(string? filteron = null, string? query = null,
			string? sortby = null, bool isAscending = true,
			int pageNumber = 1, int pageSize = 1000 )
		{
			var walks = _db.Walks.Include("Difficulty").Include("Region");
			//filtering
			if (!string.IsNullOrWhiteSpace(filteron) && !string.IsNullOrWhiteSpace(query))
			{
				if (filteron.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					return walks.Where(w => w.Name.Contains(query));
				}
			}
			//sorting
			if (!string.IsNullOrWhiteSpace(sortby))
			{
				if (sortby.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
						return isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
				}
				if (sortby.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
						return isAscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
				}
			}
			//pagination
			return await walks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
		}
		public async Task<Walk> CreateAsync(Walk walk)
		{
			await _db.Walks.AddAsync(walk);
			await _db.SaveChangesAsync();
			return await _db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == walk.Id);
		}

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var walkDomin = await _db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
			if (walkDomin == null)
			{
				return null;
			}
			_db.Walks.Remove(walkDomin);
			await _db.SaveChangesAsync();
			return walkDomin;
		}
		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			return await _db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i=>i.Id==id);
		}

		public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
		{
			var walkDomin = await _db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
			if (walkDomin == null)
			{
				return null;
			}
			walkDomin.Name = walk.Name;
			walkDomin.Description = walk.Description;
			walkDomin.LengthInKm = walk.LengthInKm;
			walkDomin.WalkPhotoUrl = walk.WalkPhotoUrl;
			walkDomin.RegionId = walk.RegionId;
			walkDomin.DifficultyId = walk.DifficultyId;
			await _db.SaveChangesAsync();
			return walkDomin;
		}
	}
}
