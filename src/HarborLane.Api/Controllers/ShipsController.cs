using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HarborLane.Domain.Models;
using HarborLane.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HarborLane.Api.Controllers
{
    /// <summary>
    /// Ships information API.
    /// </summary>
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class ShipsController : Controller
    {
        private readonly ILogger<ShipsController> _logger;
        private readonly IShipRepository _shipRepository;

        public ShipsController(ILogger<ShipsController> logger, IShipRepository shipRepository)
        {
            _logger = logger;
            _shipRepository = shipRepository;
        }
        
        /// <summary>
        /// Get all ships information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Ship>> Get()
        {
            var ships = await _shipRepository.GetShipsAsync();
            return ships;
        }
        
        /// <summary>
        /// Get a ship information based on her ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ship>> GetById(string id)
        {
            var ship = await _shipRepository.GetShipAsync(id);
            
            if (ship == null)
            {
                _logger.Log(LogLevel.Warning, $"Ship ID: {id} has not been found.");
                return NotFound();
            }

            return ship;
        }
    }
}