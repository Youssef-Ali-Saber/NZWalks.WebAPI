using NZWalks.WebAPI.Models.Domain;

namespace NZWalks.WebAPI.Repositories
{
	public interface IWalkRepository
	{
		Task<IEnumerable<Walk>> GetAllAsync(string? filteron=null , string? query=null,
			string? sortby = null, bool isAscending=true ,
			int pageNumber = 1 , int pageSize = 1000);
		Task<Walk?> GetByIdAsync(Guid id);
		Task<Walk?> CreateAsync(Walk walk);
		Task<Walk?> UpdateAsync(Guid id,Walk walk);
		Task<Walk?> DeleteAsync(Guid id);
	}
}
