using System.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VacationManager.DomainServices.Context
{
    public class AuthContext : IdentityDbContext
    {
        public AuthContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString)
        {
        }
    }
}
