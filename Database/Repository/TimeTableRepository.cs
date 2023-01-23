using Database.Converters;
using Domain.Logic;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repository
{
    public class TimeTableRepository : ITimeTableRepository
    {
        private readonly ApplicationContext context;

        public TimeTableRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public TimeTable Create(TimeTable item, Doctor doctor)
        {
            return context.Add(item.ToModel()).Entity.ToDomain();
        }

        public TimeTable Create(TimeTable item)
        {
            return Create(item, new Doctor());
        }

        public TimeTable? Delete(int id)
        {
            var timetable = GetItem(id);
            if (timetable == default)
                return null;
            return context.Remove(timetable.ToModel()).Entity.ToDomain();
        }

        public IEnumerable<TimeTable> GetAll()
        {
            return context.TimeTables.Select(item => item.ToDomain());
        }

        public TimeTable? GetItem(int id)
        {
            return context.TimeTables.FirstOrDefault(item => item.Id == id)?.ToDomain();
        }

        public TimeTable? GetItem(Doctor doctor)
        {
            return context.TimeTables.FirstOrDefault(item => item.DoctorId == doctor.Id)?.ToDomain();
        }

        public void Save()
        {
            context.SaveChangesAsync();
        }

        public TimeTable? Update(TimeTable item)
        {
            try
            {
                return context.TimeTables.Update(item.ToModel()).Entity.ToDomain();
            }
            catch
            {
                return null;
            }
        }
    }
}