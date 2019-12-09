using System.Threading.Tasks;
using HarborLane.Domain.Crawlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public ActionsController(ILogger<ActionsController> logger, IShipsCrawler shipsCrawler)
        {
            _logger = logger;
            _shipsCrawler = shipsCrawler;
        }
        
        /// <summary>
        /// Launch the crawling of ships. This action can be performed once every hour.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CrawlWiki()
        {
            _logger.Log(LogLevel.Information, "Requete de crawling sur le Wiki");
            return Ok(await _shipsCrawler.GetAllShipsAsync());
        }
    }
}