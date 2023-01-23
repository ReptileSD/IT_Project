using Domain.Logic;
using System;


namespace Domain.Models
{
    public class Appointment
    {
        public int Id;
        public DateTime StartDate;
        public DateTime EndDate;
        public int DoctorId;
        public int PatientId;

        public Appointment(int id, DateTime startdate, DateTime enddate, int patientId, int doctorId)
        {
            Id = id;
            StartDate = startdate;
            EndDate = enddate;
            PatientId = patientId;
            DoctorId = doctorId;
        }
        public Appointment(DateTime startdate, DateTime enddate, int patientId, int doctorId)
    : this(0, startdate, enddate, patientId, doctorId) { }

        public Appointment() : this(0, DateTime.MinValue, DateTime.MinValue, 0, 0) { }
        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Incorrect ID");
            if (PatientId < 0)
                return Result.Fail("Incorrect patient ID.");
            if (DoctorId < 0)
                return Result.Fail("Incorrect doctor ID.");
            if (StartDate > EndDate)
                return Result.Fail("Incorrect time provided.");
            return Result.Ok();
        }


    }
}
