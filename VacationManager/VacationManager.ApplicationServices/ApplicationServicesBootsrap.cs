using Autofac;
using VacationManager.ApplicationServices.Processors;
using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Repositories;
using VacationManager.DomainServices.Repositories.Interfaces;
using VacationManager.DomainServices.UnitOfWotrk;

namespace VacationManager.ApplicationServices
{
    public class ApplicationServicesBootsrap : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<VacationProcessor>().As<IVacationProcessor>();
            containerBuilder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            containerBuilder.RegisterType<VacationRepository>().As<IVacationRepository>();
            containerBuilder.RegisterType<PositionRepository>().As<IPositionRepository>();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            containerBuilder.RegisterType<DatabaseContext>().As<IDatabaseContext>();
        }
    }
}
