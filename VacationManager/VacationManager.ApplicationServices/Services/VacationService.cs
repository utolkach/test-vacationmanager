using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Processors;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.UnitOfWotrk;

namespace VacationManager.ApplicationServices.Services
{
    public class VacationService : BaseService, IVacationService
    {
        private readonly IVacationProcessor _vacationProcessor;

        public VacationService(IVacationProcessor vacationProcessor,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(mapper, unitOfWork)
        {
            _vacationProcessor = vacationProcessor;
        }

        public ServiceResult<VacationModel> Create(VacationModel newVacationModel)
        {
            var validationResult = _vacationProcessor.CanVacationBeCreated(newVacationModel);

            if (validationResult.BadRequest)
                return validationResult;

            var vacationEntity = _mapper.Map<Vacation>(newVacationModel);
            _unitOfWork.VacationRepository.Create(vacationEntity);
            _unitOfWork.Save();
            return new ServiceResult<VacationModel>(_mapper.Map<VacationModel>(vacationEntity));
        }

        public ServiceResult<VacationModel> Delete(Guid vacationId)
        {
            var vacation = _unitOfWork.VacationRepository.Get(vacationId);
            if (vacation == null)
            {
                return new ServiceResult<VacationModel>() { NotFound = true };
            }

            if (!vacation.IsInFuture())
            {
                return new ServiceResult<VacationModel>("You can't delete past or ongoing vacations");
            }
            _unitOfWork.VacationRepository.Delete(vacation);
            _unitOfWork.Save();
            return new ServiceResult<VacationModel>(_mapper.Map<VacationModel>(vacation));
        }

        public ServiceResult<VacationModel> Edit(VacationModel newVacationModel)
        {
            var validationResult = _vacationProcessor.CanVacationBeCreated(newVacationModel);

            if (!validationResult.BadRequest)
                return validationResult;

            var vacation = _unitOfWork.VacationRepository.Get(newVacationModel.Id);
            if (vacation == null)
            {
                return new ServiceResult<VacationModel>() { NotFound = true };
            }

            if (!vacation.IsInFuture())
            {
                return new ServiceResult<VacationModel>("You can't edit past or ongoing vacations");
            }

            vacation.Start = newVacationModel.Start;
            vacation.End = newVacationModel.End;
            _unitOfWork.VacationRepository.Update(vacation);
            _unitOfWork.Save();
            return new ServiceResult<VacationModel>(_mapper.Map<VacationModel>(vacation));
        }

        public IEnumerable<VacationModel> Search(DateTime? start, DateTime? end, string sortField, bool? desc)
        {
            var vacations = _unitOfWork.VacationRepository
                .Where(x => (!start.HasValue || x.Start >= start) && (!end.HasValue || x.End <= end)).ToList();
            var searchResult = _mapper.Map<IEnumerable<VacationModel>>(vacations);
            return sortField != null ? searchResult.OrderBy(sortField, desc) : searchResult;
        }
    }
}
