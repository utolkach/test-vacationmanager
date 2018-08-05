using VacationManager.ApplicationServices.Models;

namespace VacationManager.ApplicationServices.Processors
{
    public interface IVacationProcessor
    {
        ServiceResult<VacationModel> CanVacationBeCreated(VacationModel vacation);
    }
}
