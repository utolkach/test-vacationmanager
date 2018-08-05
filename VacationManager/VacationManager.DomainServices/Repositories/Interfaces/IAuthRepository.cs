using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VacationManager.DomainServices.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        IdentityResult RegisterUser(string userName, string password);

        IdentityUser FindUser(string userName, string password);
    }
}