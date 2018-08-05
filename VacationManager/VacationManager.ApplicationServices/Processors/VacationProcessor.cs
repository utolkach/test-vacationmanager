using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AutoMapper;
using VacationManager.ApplicationServices.Models;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.UnitOfWotrk;

namespace VacationManager.ApplicationServices.Processors
{
    public class VacationProcessor : IVacationProcessor
    {
        private static readonly int MinimumVacationLength = int.Parse(ConfigurationManager.AppSettings["MinimumVacationLength"]);
        private static readonly int MaximumVacationLength = int.Parse(ConfigurationManager.AppSettings["MaximumVacationLength"]);
        private static readonly int YearVacationLimit = int.Parse(ConfigurationManager.AppSettings["YearVacationLimit"]);
        private static readonly double SamePositionEmployeeVacationThreshold = double.Parse(ConfigurationManager.AppSettings["SamePositionEmployeeVacationThreshold"]);


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacationProcessor(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ServiceResult<VacationModel> CanVacationBeCreated(VacationModel vacation)
        {
            var result = new ServiceResult<VacationModel>()
            {
                BadRequest = false
            };

            CheckEndLaterThenStart(vacation, result);
            CheckIfIsFuture(vacation, result);
            CheckConflictWithExistingVacation(vacation, result);
            CheckMinimumLength(vacation, result);
            CheckMaximumLength(vacation, result);
            var employeeVacations = _unitOfWork.VacationRepository.Where(x => x.EmployeeId == vacation.EmployeeId).ToList();
            CheckYearLimit(vacation, employeeVacations, result);
            CheckWorkingPeriod(vacation, employeeVacations, result);
            CheckPositionsOnVacation(vacation, result);

            if (result.Messages.Any())
            {
                result.BadRequest = true;
            }

            return result;
        }

        private void CheckIfIsFuture(VacationModel vacation, ServiceResult<VacationModel> result)
        {
            if (vacation.Start < DateTime.Today)
            {
                result.Messages.Add("You can't add vacations for past dates");
            }
        }

        private void CheckConflictWithExistingVacation(VacationModel vacation, ServiceResult<VacationModel> result)
        {
            var employeeVacations = _unitOfWork.VacationRepository.Where(x => x.EmployeeId == vacation.EmployeeId &&
                                                                              (vacation.Start <= x.Start && x.Start <= vacation.End ||
                                                                               vacation.Start <= x.End && x.End <= vacation.End));
            if (employeeVacations.Any())
            {
                result.Messages.Add("employee already has a vacation in this period");
            }
        }

        private void CheckPositionsOnVacation(VacationModel vacation, ServiceResult<VacationModel> result)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(vacation.EmployeeId);
            var otherEmployeesInPosition = _unitOfWork.EmployeeRepository
                .Where(x => x.PositionId == employee.PositionId && x.Id != vacation.EmployeeId).ToList();
            var otherEmployeesOnVacation = otherEmployeesInPosition
                .Count(x => x.Vacations.Any(y => (vacation.Start <= y.Start && y.Start <= vacation.End) ||
                                                 (vacation.Start <= y.End && y.End <= vacation.End)));


            if ((int)((otherEmployeesInPosition.Count() + 1) * SamePositionEmployeeVacationThreshold) < otherEmployeesOnVacation + 1)
            {
                result.Messages.Add($"Not more than {SamePositionEmployeeVacationThreshold * 100}% employees with same position can get vacation on this period");
            }
        }

        private void CheckWorkingPeriod(VacationModel vacation, List<Vacation> employeeVacations, ServiceResult<VacationModel> result)
        {
            var lastVacation = employeeVacations.Where(x=>x.End < vacation.Start).OrderBy(x => x.Start).LastOrDefault();
            if (lastVacation != null)
            {
                var minimumWorkPeriod =  lastVacation.GetLength();

                if ((vacation.Start - lastVacation.End).Days <= minimumWorkPeriod)
                {
                    result.Messages.Add(
                        $"You can start new vacation only after {minimumWorkPeriod} days after end your last vacation");
                }
            }

            var nextVacation = employeeVacations.Where(x => x.Start > vacation.End).OrderBy(x => x.Start).FirstOrDefault();
            if (nextVacation != null)
            {
                var minimumWorkPeriod = vacation.GetLength();

                if ((nextVacation.Start - vacation.End).Days <= minimumWorkPeriod)
                {
                    result.Messages.Add(
                        $"You can end new vacation only before {minimumWorkPeriod} days before end your last vacation");
                }
            }
        }

        private void CheckYearLimit(VacationModel vacation, List<Vacation> employeeVacations,
            ServiceResult<VacationModel> result)
        {
            var validationMessage = $"Vacation limit exceeded. Year limit for vacation length is {YearVacationLimit} days";
            var employeeVacationsWithPlanned = employeeVacations.ToList();
            employeeVacationsWithPlanned.Add(_mapper.Map<Vacation>(vacation));
            var yearVacationSum = employeeVacationsWithPlanned
                .Where(x => x.Start.Year == vacation.Start.Year || x.End.Year == vacation.Start.Year)
                .Sum(x => x.GetDaysForYear(vacation.Start.Year));
            if (vacation.Start.Year == vacation.End.Year)
            {
                if (yearVacationSum > YearVacationLimit)
                {
                    result.Messages.Add(validationMessage);
                }
            }
            else
            {
                var startYearVacationSum = yearVacationSum;
                var endYearVacationSum = employeeVacationsWithPlanned
                    .Where(x => x.Start.Year == vacation.Start.Year || x.End.Year == vacation.Start.Year)
                    .Sum(x => x.GetDaysForYear(vacation.Start.Year));
                if (startYearVacationSum > YearVacationLimit || endYearVacationSum > YearVacationLimit)
                {
                    result.Messages.Add(validationMessage);

                }
            }
        }

        private void CheckMaximumLength(VacationModel vacation, ServiceResult<VacationModel> result)
        {
            if (vacation.GetLength() > MaximumVacationLength)
                result.Messages.Add($"Vacation can't be longer than {MaximumVacationLength} days");
        }

        private void CheckMinimumLength(VacationModel vacation, ServiceResult<VacationModel> result)
        {
            if (vacation.GetLength() > 0 && vacation.GetLength() < MinimumVacationLength)
                result.Messages.Add($"Minimum length is {MinimumVacationLength} days");
        }

        private void CheckEndLaterThenStart(VacationModel vacation, ServiceResult<VacationModel> result)
        {
            if (vacation.Start > vacation.End)
                result.Messages.Add("Start can't be later then End");
        }
    }
}