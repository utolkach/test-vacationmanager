using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.DomainServices.Repositories;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.ApplicationServices.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public AuthService()
        {
            this._authRepository = new AuthRepository();
        }

        public IdentityResult Register(string login, string password)
        {
            return _authRepository.RegisterUser(login, password);
        }

        public IdentityUser Get(string login, string password)
        {
            return _authRepository.FindUser(login, password);
        }
    }
}