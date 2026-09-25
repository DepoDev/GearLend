using GearLend.Application.DTOs;
using GearLend.Application.Features.RentalRequests.Commands.CreateRentalRequest;
using GearLend.Application.Features.RentalRequests.Queries.GetAllRentalRequests;
using GearLend.Application.Features.RentalRequests.Queries.GetRentalRequestById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GearLend.API.Controllers
{
    /// <summary>
    /// Manages rental requests for equipment and gear.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RentalRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new rental request for an asset.
        /// </summary>
        /// <param name="dto">Rental request details (AssetId, UserId, StartDate, EndDate)</param>
        /// <returns>The ID of the newly created rental request.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateRentalRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Invalid rental request payload." });
            }

            try
            {
                var command = new CreateRentalRequestCommand
                {
                    AssetId = dto.AssetId,
                    UserId = dto.UserId,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate
                };

                var id = await _mediator.Send(command);
                return Ok(new { id });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all rental requests.
        /// </summary>
        /// <returns>A list of all rental requests.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _mediator.Send(new GetAllRentalRequestsQuery());
            return Ok(new { requests });
        }

        /// <summary>
        /// Gets a rental request by its unique identifier.
        /// </summary>
        /// <param name="id">The rental request Guid.</param>
        /// <returns>The rental request details.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = await _mediator.Send(new GetRentalRequestByIdQuery { Id = id });
            if (request == null)
            {
                return NotFound(new { message = $"Rental request with ID '{id}' was not found." });
            }

            return Ok(new { request });
        }
    }
}
