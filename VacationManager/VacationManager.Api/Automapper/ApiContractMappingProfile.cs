using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.Models;

namespace VacationManager.Automapper
{
    public class ApiContractMappingProfile : Profile
    {
        public ApiContractMappingProfile()
        {
            CreateMap<EmployeeApiContract, EmployeeModel>();
            CreateMap<EmployeeModel, EmployeeApiContract>();
            CreateMap<VacationApiContract, VacationModel>();
            CreateMap<VacationModel, VacationApiContract>();
        }
    }
}