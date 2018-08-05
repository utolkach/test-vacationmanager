using System;
using System.Collections.Generic;
using VacationManager.ApplicationServices.Models;

namespace VacationManager.ApplicationServices.Services.Interfaces
{
    public interface IVacationService
    {
        ServiceResult<VacationModel> Create(VacationModel newVacationModel);
        ServiceResult<VacationModel> Delete(Guid vacationId);
        ServiceResult<VacationModel> Edit(VacationModel map);
        IEnumerable<VacationModel> Search(DateTime? start, DateTime? end, string sortField, bool? desc);
    }
}
