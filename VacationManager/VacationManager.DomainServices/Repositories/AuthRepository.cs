using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthContext _ctx;

        private readonly UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public IdentityResult RegisterUser(string userName, string password)
        {
            var user = new IdentityUser
            {
                UserName = userName
            };

            return _userManager.Create(user, password);
        }

        public IdentityUser FindUser(string userName, string password)
        {
            return _userManager.Find(userName, password);
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}
