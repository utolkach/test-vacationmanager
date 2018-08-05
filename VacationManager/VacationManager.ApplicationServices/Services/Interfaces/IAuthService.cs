using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VacationManager.ApplicationServices.Services.Interfaces
{
    public interface IAuthService
    {
        IdentityResult Register(string login, string password);
        IdentityUser Get(string login, string password);
    }
}
