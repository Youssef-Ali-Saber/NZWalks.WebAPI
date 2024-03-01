using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.WebAPI.Models.Domain;
using NZWalks.WebAPI.Models.DTO;
using NZWalks.WebAPI.Repositories;
using NZWalks.WebAPI.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace NZWalks.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class WalksController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IWalkRepository _walkRepository;

		public WalksController(IMapper mapper,IWalkRepository walkRepository)
		{
			_mapper = mapper;
			_walkRepository = walkRepository;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll(string? filterOn , string? query,string? sortBy , bool? isAscending
			,int pageNumber =1,int pageSize=1000)
		{
			var walks = await _walkRepository.GetAllAsync(filterOn,query,sortBy,isAscending??true,pageNumber,pageSize);
			return Ok(_mapper.Map<List<WalkDto>>(walks));
		}
		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var walk = await _walkRepository.GetByIdAsync(id);
			if (walk == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<WalkDto>(walk));
		}
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create(AddWalkRequestDTO addWalkRequestDTO)
		{
			var walk = _mapper.Map<Walk>(addWalkRequestDTO);
			await _walkRepository.CreateAsync(walk);
			return Ok(_mapper.Map<WalkDto>(walk));
		}
		[HttpPut("{id:Guid}")]
		[ValidateModel]
		public async Task<IActionResult> Update(Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
		{
			var walk = await _walkRepository.GetByIdAsync(id);
			if (walk == null)
			{
				return NotFound();
			}
			_mapper.Map(updateWalkRequestDTO, walk);
			await _walkRepository.UpdateAsync(id,walk);
			return Ok(_mapper.Map<WalkDto>(walk));
		}
		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var walk = await _walkRepository.GetByIdAsync(id);
			if (walk == null)
			{
				return NotFound();
			}
			await _walkRepository.DeleteAsync(id);
			return Ok(_mapper.Map<WalkDto>(walk));
		}
	}
}
