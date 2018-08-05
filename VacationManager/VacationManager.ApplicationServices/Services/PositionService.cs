using System.Collections.Generic;
using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Services.Interfaces;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.UnitOfWotrk;

namespace VacationManager.ApplicationServices.Services
{
    public class PositionService : BaseService, IPositionService
    {

        public PositionService(IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(mapper, unitOfWork)
        {
        }

        public IEnumerable<PositionModel> GetPositions()
        {
            var positions = _unitOfWork.PositionRepository.Get();
            return _mapper.Map<IEnumerable<PositionModel>>(positions);
        }

        public PositionModel CreatePosition(string title)
        {
            var position = new Position() { Title = title };
            _unitOfWork.PositionRepository.Create(position);
            _unitOfWork.Save();
            return _mapper.Map<PositionModel>(position);
        }
    }
}