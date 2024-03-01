using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.WebAPI.CustomActionFilters;
using NZWalks.WebAPI.Models.Domain;
using NZWalks.WebAPI.Models.DTO;
using NZWalks.WebAPI.Repositories;


namespace NZWalks.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class RegionsController : ControllerBase
	{
		private readonly IRegionRepository _repository;
		private readonly IMapper _mapper;
		private readonly ILogger<RegionsController> _logger;

		public RegionsController(IRegionRepository repository,IMapper mapper,ILogger<RegionsController> logger)
        {
			_repository = repository;
			_mapper = mapper;
			_logger = logger;
		}
        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var regionsDomin = await _repository.GetAllAsync();
			    return Ok(_mapper.Map<List<RegionDto>>(regionsDomin));
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Something went wrong in the {nameof(GetAll)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}


        } 
		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var regionDomin = await _repository.GetByIdAsync(id);
			if (regionDomin == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<RegionDto>(regionDomin));
		}
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create(AddRegionRequestDTO addRegionRequestDto)
		{
			
			var regionDomin = _mapper.Map<Region>(addRegionRequestDto);
			regionDomin = await _repository.CreateAsync(regionDomin);
			var regionDto = _mapper.Map<RegionDto>(regionDomin);
			return CreatedAtAction(nameof(GetById), new { id = regionDomin.Id }, regionDto);
		}
		[HttpPut("{id:Guid}")]
		[ValidateModel]
		public async Task<IActionResult> Update(Guid id, UpdateRegionRequestDTO updateRegionRequestDto)
		{
			var regionDomin = _mapper.Map<Region>(updateRegionRequestDto);
			regionDomin = await _repository.UpdateAsync(id, regionDomin);
			if (regionDomin == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<RegionDto>(regionDomin));
		}
		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var regionDomin = await _repository.DeleteAsync(id);
			if (regionDomin == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<RegionDto>(regionDomin));
		}
	}
}
