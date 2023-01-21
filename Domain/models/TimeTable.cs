using Domain.Logic;
using System;


namespace Domain.Models
{
    public class TimeTable
    {
        public int DoctorId;
        public DateTime StartDate;
        public DateTime EndDate;



        public TimeTable(int doctorId, DateTime startdate, DateTime enddate)
        {
            DoctorId = doctorId;
            StartDate = startdate;
            EndDate = enddate;
        }

        public TimeTable() : this(0, DateTime.MinValue, DateTime.MaxValue) { }

        public Result IsValid()
        {
            if (DoctorId < 0)
                return Result.Fail("Incorrect doctor id.");
            if (StartDate > EndDate)
                return Result.Fail("Incorrect date.");
            return Result.Ok();


        }
    }
}
