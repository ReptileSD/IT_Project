using Domain.Models;
using System.Collections.Generic;

namespace Domain.Logic
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool CreateAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAppointments(int DoctorId);
        IEnumerable<Appointment> GetAppointments(Specialization specialization);
    }
}