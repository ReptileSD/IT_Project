using Domain.Logic;
using Domain.Models;
using System.Collections.Generic;

namespace Domain.UseCases
{
    public class TimeTableInteractor
    {
        private readonly ITimeTableRepository _db;
        private readonly IDoctorRepository _doctor_db;

        public TimeTableInteractor(ITimeTableRepository db, IDoctorRepository doctor_db)
        {
            _db = db;
            _doctor_db = doctor_db;
        }

        public Result<TimeTable> GetTimeTable(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.isFailure)
                return Result.Fail<TimeTable>("Cannot get timetable");
            return Result.Ok(_db.GetItem(doctor.Id)!);
        }

        public Result<TimeTable> GetTimeTable(int timetableId)
        {
            var res = _db.GetItem(timetableId);
            return res != null ? Result.Ok(res) : Result.Fail<TimeTable>("Cannot find timetable with this ID");
        }

        public Result<TimeTable> CreateTimeTable(int doctor_id, TimeTable timetable)
        {
            var doctor = _doctor_db.GetItem(doctor_id);
            if (doctor == default)
                return Result.Fail<TimeTable>("There is no doctor with this ID");

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
        public Result<TimeTable> UpdateTimeTable(TimeTable timetable)
        {
            var result = timetable.IsValid();
            if (result.isFailure)
                return Result.Fail<TimeTable>("Invalid timetable: " + result.Error);
            var res = _db.Update(timetable);
            if (res != null)
            {
                _db.Save();
                return Result.Ok(res);
            }
            return Result.Fail<TimeTable>("Unable to update timetable");
        }

        public Result<TimeTable> DeleteTimeTable(int id)
        {
            var result = GetTimeTable(id);
            if (result.isFailure)
                return Result.Fail<TimeTable>(result.Error);
            if (_db.Delete(id)!.IsValid().Success)
            {
                _db.Save();
                return result;
            }
            return Result.Fail<TimeTable>("Cannot delete the timetable");
        }
    }
}