using System.Reflection;
using System.Web.Http;
using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.Models;

namespace VacationManager.Controllers
{
    [Authorize]
    public class EmployeeController : BaseApiController
    {
        private IEmployeeService employeeService { get; }

        public EmployeeController(
            IEmployeeService employeeService,
            IMapper mapper) : base(mapper)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            var employeeList = this.employeeService.Get();
            return Ok(employeeList);
        }

        [HttpGet]
        [Route("api/employees")]
        public IHttpActionResult SearchAndSort(string filter, string sortField = null, bool? desc = null)
        {
            if (sortField != null)
            {
                var propertyInfo = typeof(EmployeeModel).GetProperty(sortField,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (propertyInfo == null)
                {
                    return BadRequest("There is no such field to sort");
                }
            }

            var employeeList = this.employeeService.Search(filter, sortField, desc);
            return Ok(employeeList);
        }

        [HttpPost]
        [Route("api/employees")]
        public IHttpActionResult Create([FromBody]EmployeeApiContract value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var result = this.employeeService.Create(mapper.Map<EmployeeModel>(value));
            if (result.NotFound)
            {
                return NotFound();
            }

            return Ok(result.Result);
        }
    }
}