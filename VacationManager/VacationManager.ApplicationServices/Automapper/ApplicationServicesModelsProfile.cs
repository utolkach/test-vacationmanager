using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.DomainServices.Entities;

namespace VacationManager.ApplicationServices.Automapper
{
    internal class ApplicationServicesModelsProfile : Profile
    {
        public ApplicationServicesModelsProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                .ForMember(x => x.Position, o => o.MapFrom(s => s.Position.Title));
            CreateMap<EmployeeModel, Employee>();
            CreateMap<VacationModel, Vacation>();
            CreateMap<Vacation, VacationModel>();
        }
    }
}
