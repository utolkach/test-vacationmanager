using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.UnitOfWotrk;

namespace VacationManager.ApplicationServices.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(mapper, unitOfWork)
        {
        }

        public ServiceResult<EmployeeModel> Create(EmployeeModel newEmployeeModel)
        {
            if (_unitOfWork.PositionRepository.Get(newEmployeeModel.PositionId) == null)
            {
                return new ServiceResult<EmployeeModel>("Selected position not found") { NotFound = true };
            }

            var newEmployee = _mapper.Map<Employee>(newEmployeeModel);
            _unitOfWork.EmployeeRepository.Create(newEmployee);
            _unitOfWork.EmployeeRepository.SaveChanges();
            return new ServiceResult<EmployeeModel>(_mapper.Map<EmployeeModel>(newEmployee));
        }

        public IEnumerable<EmployeeModel> Search(string filter, string sortField = null, bool? desc = null)
        {
            filter = string.IsNullOrWhiteSpace(filter) ? null : filter;
            var employees = _unitOfWork.EmployeeRepository.Where(x => string.IsNullOrEmpty(filter) ||
                                                           (x.FirstName.Contains(filter) ||
                                                            x.LastName.Contains(filter) ||
                                                            x.MiddleName.Contains(filter) ||
                                                            x.Position.Title.Contains(filter)));
            var employeeModels = _mapper.Map<IEnumerable<EmployeeModel>>(employees);
            return sortField != null ? employeeModels.OrderBy(sortField, desc) : employeeModels;
        }

        public IEnumerable<EmployeeModel> Get()
        {
            var employees = _unitOfWork.EmployeeRepository.Get().ToList();
            var mappedResult = _mapper.Map<IEnumerable<EmployeeModel>>(employees);
            return mappedResult;
        }
    }
}
