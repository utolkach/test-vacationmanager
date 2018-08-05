using AutoMapper;
using AutoMapper.Configuration;
using VacationManager.ApplicationServices;
using VacationManager.Automapper;

namespace VacationManager
{
    public static class MappingsRegistration
    {
        public static MapperConfigurationExpression GetMapperConfigurationExpression()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            var assembly = typeof(ApiContractMappingProfile).Assembly;
            mapperConfigurationExpression.AddProfiles(assembly);
            assembly = typeof(ApplicationServicesBootsrap).Assembly;
            mapperConfigurationExpression.AddProfiles(assembly);

            return mapperConfigurationExpression;
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(GetMapperConfigurationExpression());
            return mapperConfiguration;
        }

        public static void InitializeMapper()
        {
            Mapper.Initialize(GetMapperConfigurationExpression());
        }
    }
}