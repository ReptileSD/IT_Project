using Microsoft.AspNetCore.Mvc;
using Domain.UseCases;
using Domain.Models;
using System;


namespace IT_Project.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : Controller
    {
        private readonly AppointmentInteractor _appointments;
        private readonly TimeTableInteractor _timetables;
        public AppointmentController(AppointmentInteractor appointment, TimeTableInteractor timetable)
        {
            _timetables = timetable;
            _appointments = appointment;
        }
        [HttpPost("save")]
        public IActionResult SaveAppointment(int patientId,
            int doctorId,
            DateTime start_time,
            DateTime end_time,
            int timetable_id)
        {
            Appointment appointment = new Appointment(start_time, end_time, patientId, doctorId);
            var timetable = _timetables.GetTimeTable(timetable_id);
            if (timetable.isFailure)
            {
                return Problem(statusCode: 404, detail: timetable.Error);
            }

            var res = _appointments.SaveAppointment(appointment, timetable.Value);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }

        [HttpGet("get")]
        public IActionResult GetAppointments(int specialization_id)
        {
            Specialization spec = new Specialization(specialization_id, "tmp");
            var res = _appointments.GetAppointments(spec);

            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
    }
}