using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace IT_Project.Controllers
{
    [Route("api/timetable")]
    [ApiController]
    public class TimeTableController : ControllerBase
    {
        private readonly TimeTableInteractor _timetables;
        private readonly DoctorInteractor _doctors;

        public TimeTableController(TimeTableInteractor timetables, DoctorInteractor doctors)
        {
            _timetables = timetables;
            _doctors = doctors;
        }
        [Authorize]
        [HttpPost("create")]
        public IActionResult AddTimeTable(int doctor_id, DateTime start_time, DateTime end_time)
        {
            TimeTable timetable = new TimeTable(doctor_id, start_time, end_time);

            var res = _timetables.CreateTimeTable(doctor_id, timetable);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
        [Authorize]
        [HttpGet("update")]
        public IActionResult UpdateTimeTable(int timetable_id, int? doctor_id, DateTime? startdate, DateTime? enddate)
        {
            var res = _timetables.GetTimeTable(timetable_id);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);

            var timetable = res.Value;

            if (doctor_id != null)
                timetable.DoctorId = (int)doctor_id;
            if (startdate != null && enddate != null)
            {
                timetable.StartDate = (DateTime)startdate;
                timetable.EndDate = (DateTime)enddate;
            }

            var updateResult = _timetables.UpdateTimeTable(timetable);

            if (updateResult.isFailure)
                return Problem(statusCode: 404, detail: updateResult.Error);

            return Ok();
        }
        [HttpGet("getById")]
        public IActionResult GetById(int timetable_id)
        {
            var res = _timetables.GetTimeTable(timetable_id);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
        [HttpGet("getByDoctorId")]
        public IActionResult GetByDoctorId(int doctor_id)
        {
            var doctor = _doctors.GetDoctor(doctor_id);
            if (doctor.isFailure)
                return Problem(statusCode: 404, detail: doctor.Error);
            var res = _timetables.GetTimeTable(doctor.Value);
            if (res.isFailure)
                return Problem(statusCode: 404, detail: res.Error);
            return Ok(res.Value);
        }
        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteTimeTable(int id)
        {
            var res = _timetables.DeleteTimeTable(id);
            if (res.isFailure)
                return Problem(statusCode: 400, detail: res.Error);
            return Ok(res.Value);
        }
    }
}