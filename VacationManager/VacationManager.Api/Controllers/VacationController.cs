using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.Models;

namespace VacationManager.Controllers
{
    [Authorize]
    public class VacationController : BaseApiController
    {
        private readonly IVacationService _vacationService;

        public VacationController(IVacationService vacationService, IMapper mapper) : base(mapper)
        {
            _vacationService = vacationService;
        }

        [HttpGet]
        [Route("api/vacations")]
        public IHttpActionResult Get(DateTime? start = null, DateTime? end = null, string sortField = null, bool? desc = null)
        {
            if (sortField != null)
            {
                var propertyInfo = typeof(VacationModel).GetProperty(sortField, 
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (propertyInfo == null)
                {
                    return BadRequest("There is no such field to sort");
                }
            }

            IEnumerable<VacationModel> result = _vacationService.Search(start, end, sortField, desc);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/vacations")]
        public IHttpActionResult Post([FromBody]VacationApiContract value)
        {
            var result = _vacationService.Create(mapper.Map<VacationModel>(value));
            return ReturnResult(result, mapper.Map<VacationApiContract>(result.Result));
        }

        [HttpPut]
        [Route("api/vacations/{id}")]
        public IHttpActionResult Put(Guid id, [FromBody]VacationApiContract vacation)
        {
            vacation.Id = id;
            var result = _vacationService.Edit(mapper.Map<VacationModel>(vacation));
            return ReturnResult(result, mapper.Map<VacationApiContract>(result.Result));
        }

        [HttpDelete]
        [Route("api/vacations/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var result = _vacationService.Delete(id);
            return ReturnResult(result);
        }
    }
}