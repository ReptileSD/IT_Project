using Domain.Models;
using System.Collections.Generic;

namespace Domain.Logic.Interfaces
{
    public interface ITimeTableRepository : IRepository<TimeTable>
    {
        IEnumerable<TimeTable> getTimeTable(Doctor doctor);
        bool CreateTimeTable(Doctor doctor, TimeTable timetable);
        bool UpdateTimeTable(Doctor? doctor, TimeTable? timetable);
    }
}