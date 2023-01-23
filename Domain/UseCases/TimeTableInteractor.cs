using Domain.Logic;
using Domain.Models;
using Domain.Logic;
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

        public Result<TimeTable> getTimeTable(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.isFailure)
                return Result.Fail<TimeTable>("Cannot get timetable");
            return Result.Ok(_db.GetItem(doctor.Id)!);
        }

        public Result<TimeTable> CreateTimeTable(Doctor doctor, TimeTable timetable)
        {
            var result = doctor.IsValid() & timetable.IsValid();
            if (!result)
                return Result.Fail<TimeTable>("Cannot create timetable");
            if (_db.Create(timetable).Id >= 0)
            {
                _db.Save();
                return Result.Ok(timetable);
            }
            return Result.Fail<TimeTable>("Unable to add timetable");
        }

    }
}