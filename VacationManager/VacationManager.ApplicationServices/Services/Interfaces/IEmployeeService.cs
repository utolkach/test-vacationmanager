using System.Collections.Generic;
using VacationManager.ApplicationServices.Models;

namespace VacationManager.ApplicationServices.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeModel> Get();
        ServiceResult<EmployeeModel> Create(EmployeeModel newEmployeeModel);
        IEnumerable<EmployeeModel> Search(string filter, string sortField = null, bool? desc = null);
    }
}
