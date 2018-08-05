using System.Web.Http;
using AutoMapper;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.Models;

namespace VacationManager.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService, IMapper mapper) : base(mapper)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/account")]
        public IHttpActionResult Register(UserApiContract userApiContract)
        {
            var result = _authService.Register(userApiContract.Login, userApiContract.Login);
            if (result.Succeeded)
            {
                return Ok(userApiContract);
            }

            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}
