using GearLend.Application.Interfaces;
using GearLend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GearLend.API.Controllers
{
    /// <summary>
    /// Manages available equipment assets in GearLend.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IRepository<Asset> _assetRepository;

        public AssetsController(IRepository<Asset> assetRepository)
        {
            _assetRepository = assetRepository;
        }

        /// <summary>
        /// Retrieves all assets available for rent.
        /// </summary>
        /// <returns>A list of assets.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var assets = await _assetRepository.Get().ToListAsync();
            return Ok(new { assets });
        }

        /// <summary>
        /// Creates a new asset.
        /// </summary>
        /// <param name="asset">Asset details.</param>
        /// <returns>The newly created asset.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Asset asset)
        {
            if (asset == null || string.IsNullOrWhiteSpace(asset.Name))
            {
                return BadRequest(new { message = "Asset name is required." });
            }

            asset.Id = asset.Id == Guid.Empty ? Guid.NewGuid() : asset.Id;
            asset.CreatedAt = DateTime.UtcNow;

            await _assetRepository.AddAsync(asset);
            await _assetRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = asset.Id }, asset);
        }
    }
}
