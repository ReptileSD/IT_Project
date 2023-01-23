using Domain.Logic;
using System.Linq;
using System.Collections.Generic;
using Domain.Models;
using System.Threading;

namespace Domain.UseCases
{
    public class AppointmentInteractor
    {
        private readonly IAppointmentRepository _db;
        private static readonly Dictionary<int, Mutex> _mutexDictionary = new Dictionary<int, Mutex>();

        public AppointmentInteractor(IAppointmentRepository db)
        {
            _db = db;
        }

        public Result<Appointment> SaveAppointment(Appointment appointment, TimeTable timetable)
        {
            var result = appointment.IsValid();
            if (result.isFailure)
                return Result.Fail<Appointment>("Invalid appointment: " + result.Error);

            var result1 = timetable.IsValid();
            if (result1.isFailure)
                return Result.Fail<Appointment>("Invalid schedule: " + result1.Error);

            if (timetable.StartDate > appointment.StartDate || timetable.EndDate < appointment.EndDate)
                return Result.Fail<Appointment>("Appointment out of schedule");

            var appointments = _db.GetAppointments(appointment.DoctorId).ToList();
            appointments.Sort((a, b) => { return (a.StartDate < b.StartDate) ? -1 : 1; });
            var index = appointments.FindLastIndex(a => a.EndDate <= appointment.StartDate);
            if (appointments.Count > index + 1)
            {
                if (appointments[index + 1].StartDate < appointment.EndDate)
                    return Result.Fail<Appointment>("Appointment time already taken");
            }
            if (!_mutexDictionary.ContainsKey(appointment.DoctorId))
                _mutexDictionary.Add(appointment.DoctorId, new Mutex());
            _mutexDictionary.First(d => d.Key == appointment.DoctorId).Value.WaitOne();
            if (_db.Create(appointment).IsValid().Success)
            {
                _db.Save();
                _mutexDictionary.First(d => d.Key == appointment.DoctorId).Value.ReleaseMutex();
                return Result.Ok(appointment);
            }
            return Result.Fail<Appointment>("Unable to save appointment");
        }

        public Result<IEnumerable<Appointment>> GetAppointments(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.isFailure)
                return Result.Fail<IEnumerable<Appointment>>("Invalid specialization: " + result.Error);
            return Result.Ok(_db.GetAppointments(specialization));
        }
    }
}