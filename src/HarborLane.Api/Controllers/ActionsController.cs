using System;
using System.Threading.Tasks;
using HarborLane.Domain.Crawlers;
using HarborLane.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace HarborLane.Api.Controllers
{
    /// <summary>
    /// Crawler execution controller actions
    /// </summary>
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("actions/[action]")]
    public class ActionsController : Controller
    {
        private readonly ILogger<ActionsController> _logger;
        private readonly IShipsCrawler _shipsCrawler;
        private readonly IShipRepository _shipRepository;
        private readonly ITimeRepository _timeRepository;

        public ActionsController(ILogger<ActionsController> logger, IShipsCrawler shipsCrawler,
            IShipRepository shipRepository, ITimeRepository timeRepository)
        {
            _logger = logger;
            _shipsCrawler = shipsCrawler;
            _shipRepository = shipRepository;
            _timeRepository = timeRepository;
        }

        /// <summary>
        /// Launch the crawling of ships. This action can be performed once every hour.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CrawlWiki([FromQuery(Name = "force")] bool force = false)
        {
            _logger.Log(LogLevel.Information,
                $"Request for crawling received from {HttpContext.Connection.RemoteIpAddress} at {DateTime.Now}.");

            if (force)
            {
                await UpdateRedis();
                _logger.Log(LogLevel.Information, "Crawling performed in forced mode.");
                return Ok("Request for crawling done.");
            }

            var lastUpdate = await _timeRepository.GetTime();
            var timeInterval = DateTime.Now - lastUpdate;

            if (timeInterval.TotalHours >= 1)
            {
                await UpdateRedis();
                _logger.Log(LogLevel.Information, "Crawling performed.");
                return Ok("Request for crawling done.");
            }

            _logger.Log(LogLevel.Information, "Crawling fail.");
            return BadRequest("Request crawling ignore. The time between requests for crawling is too short (1 hour min).");
        }

        private async Task UpdateRedis()
        {
            var ships = await _shipsCrawler.GetAllShipsAsync();
            await _shipRepository.SetShipsAsync(ships);
            await _timeRepository.SetTimeAsync(DateTime.Now);
        }
    }
}
