using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using VacationManager.ApplicationServices;
using VacationManager.ApplicationServices.Services;
using VacationManager.ApplicationServices.Services.Interfaces;

namespace VacationManager
{
    public class DiBootstrap
    {
        public static IContainer Initialize(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<ApplicationServicesBootsrap>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<VacationService>().As<IVacationService>();
            builder.RegisterType<PositionService>().As<IPositionService>();
            builder.RegisterType<AuthService>().As<IAuthService>();

            // Automapper
            builder.Register(c => MappingsRegistration.GetMapperConfiguration()).AsSelf().SingleInstance();
            builder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    return context.Resolve<MapperConfiguration>().CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
            
            var container = builder.Build();
            
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}