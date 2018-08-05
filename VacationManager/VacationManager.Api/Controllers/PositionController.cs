using System.Web.Http;
using AutoMapper;
using VacationManager.ApplicationServices.Services.Interfaces;

namespace VacationManager.Controllers
{
    [Authorize]
    public class PositionController : BaseApiController
    {
        private IPositionService _positionService { get; }

        public PositionController(
            IPositionService positionService,
            IMapper mapper) : base(mapper)
        {
            this._positionService = positionService;
        }

        [HttpGet]
        [Route("api/positions")]
        public IHttpActionResult Get()
        {
            return Ok(_positionService.GetPositions());
        }

        [HttpPost]
        [Route("api/position/{title}")]
        public IHttpActionResult Create(string title)
        {
            return Ok(_positionService.CreatePosition(title));
        }

    }
}