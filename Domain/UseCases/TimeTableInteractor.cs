using Domain.Logic;
using Domain.Models;
using Domain.Logic.Interfaces;
using System.Collections.Generic;

namespace Domain.UseCases
{
    public class TimeTableInteractor
    {
        private readonly ITimeTableRepository _db;

        public TimeTableInteractor(ITimeTableRepository db)
        {
            _db = db;
        }

        public Result<IEnumerable<TimeTable>> getSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.isFailure)
                return Result.Fail<IEnumerable<TimeTable>>("Cannot delete timetable");
            return Result.Ok(_db.getTimeTable(doctor));
        }

        public Result<TimeTable> CreateSchedule(Doctor doctor, TimeTable schedule)
        {
            var result = doctor.IsValid() & schedule.IsValid();
            if (!result)
                return Result.Fail<TimeTable>("Cannot create timetable");
            return _db.CreateTimeTable(doctor, schedule) ? Result.Ok(schedule) : Result.Fail<TimeTable>("Unable to add timetable");
        }

    }
}