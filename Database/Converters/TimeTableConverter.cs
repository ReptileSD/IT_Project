using Database.Models;
using Domain.Models;


using TimeTableDB = Database.Models.TimeTable;
using TimeTableDomain = Domain.Models.TimeTable;

namespace Database.Converters
{
    public static class TimeTableConverter
    {
        public static TimeTableDB ToModel(this TimeTableDomain model)
        {
            return new TimeTableDB
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                DoctorId = model.DoctorId,
            };
        }

        public static TimeTableDomain ToDomain(this TimeTableDB model)
        {
            return new TimeTableDomain
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                DoctorId = model.DoctorId,
            };
        }
    }
}