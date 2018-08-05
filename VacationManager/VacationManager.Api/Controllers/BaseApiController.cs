using System.Web.Http;
using AutoMapper;
using VacationManager.ApplicationServices.Models;

namespace VacationManager.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IMapper mapper;

        public BaseApiController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        protected IHttpActionResult ReturnResult<T>(ServiceResult<VacationModel> result, T returnValue = null) where T : class
        {
            if (result.NotFound)
            {
                return NotFound();
            }

            if (result.BadRequest)
            {
                return BadRequest(result.GetMessage());
            }

            return Ok(returnValue);
        }

        protected IHttpActionResult ReturnResult(ServiceResult<VacationModel> result)
        {
            if (result.NotFound)
            {
                return NotFound();
            }

            if (result.BadRequest)
            {
                return BadRequest(result.GetMessage());
            }

            return Ok();
        }
    }
}
