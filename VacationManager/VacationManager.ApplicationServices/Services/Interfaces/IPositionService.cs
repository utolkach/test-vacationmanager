using System.Collections.Generic;
using VacationManager.ApplicationServices.Models;

namespace VacationManager.ApplicationServices.Services.Interfaces
{
    public interface IPositionService
    {
        IEnumerable<PositionModel> GetPositions();
        PositionModel CreatePosition(string title);
    }
}
