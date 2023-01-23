using Database.Models;
using Domain.Models;


using AppointmentDB = Database.Models.Appointment;
using AppointmentDomain = Domain.Models.Appointment;

namespace Database.Converters
{
    public static class AppointmentConverter
    {
        public static AppointmentDB ToModel(this AppointmentDomain model)
        {
            return new AppointmentDB
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId
            };
        }

        public static AppointmentDomain ToDomain(this AppointmentDB model)
        {
            return new AppointmentDomain
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId
            };
        }
    }
}