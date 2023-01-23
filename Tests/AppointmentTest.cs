﻿using Xunit;
using Moq;

using Domain.Models;
using Domain.Logic;
using Domain.UseCases;
using System;
using System.Collections.Generic;


namespace Tests
{
    public class AppointmentTest
    {
        private readonly AppointmentInteractor _service;
        private readonly Mock<IAppointmentRepository> _mock;

        public AppointmentTest()
        {
            _mock = new Mock<IAppointmentRepository>();
            _service = new AppointmentInteractor(_mock.Object);
        }

        [Fact]
        public void Save_InvalidApp()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, -2, -2);
            var sched = new TimeTable();
            var res = _service.SaveAppointment(app, sched);

            Assert.True(res.isFailure);
            Assert.Contains("Invalid appointment: ", res.Error);
        }

        [Fact]
        public void Save_InvalidSched()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, 0, 0);
            var sched = new TimeTable(-1, DateTime.MinValue, DateTime.MinValue);
            var res = _service.SaveAppointment(app, sched);

            Assert.True(res.isFailure);
            Assert.Contains("Invalid schedule: ", res.Error);
        }

        [Fact]
        public void Save_InvalidTime()
        {
            var app = new Appointment(DateTime.MaxValue, DateTime.MaxValue, 0, 0);
            var sched = new TimeTable(0, DateTime.MinValue, DateTime.MinValue);
            var res = _service.SaveAppointment(app, sched);

            Assert.True(res.isFailure);
            Assert.Contains("Appointment out of schedule", res.Error);
        }

        [Fact]
        public void Save_TakenTime()
        {
            List<Appointment> apps = new()
            {
                new Appointment(DateTime.MinValue, DateTime.Parse("2022-01-10"), 0, 0),
                new Appointment(DateTime.Parse("2022-01-10"), DateTime.MaxValue, 0, 0)
            };
            _mock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);

            var app = new Appointment(DateTime.Parse("2022-01-01"), DateTime.Parse("2022-01-20"), 0, 0);
            var sched = new TimeTable(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _service.SaveAppointment(app, sched);

            Assert.True(res.isFailure);
            Assert.Contains("Appointment time already taken", res.Error);
        }

        [Fact]
        public void Save_SaveError()
        {
            List<Appointment> apps = new();
            _mock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _mock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => new Appointment(-1, DateTime.MinValue, DateTime.MaxValue, 0, 0));
            var app = new Appointment();
            var sched = new TimeTable(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _service.SaveAppointment(app, sched);

            Assert.True(res.isFailure);
            Assert.Contains("Unable to save appointment", res.Error);
        }

        [Fact]
        public void Save_Valid()
        {
            List<Appointment> apps = new();
            _mock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _mock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => new Appointment());
            var app = new Appointment();
            var sched = new TimeTable(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _service.SaveAppointment(app, sched);

            Assert.True(res.Success);
        }

        [Fact]
        public void Get_InvalidSpec()
        {
            var spec = new Specialization();
            var res = _service.GetAppointments(spec);

            Assert.True(res.isFailure);
            Assert.Contains("Invalid specialization: ", res.Error);
        }

        [Fact]
        public void Get_Valid()
        {
            List<Appointment> apps = new()
            {
                new Appointment(),
                new Appointment()
            };
            IEnumerable<Appointment> a = apps;
            _mock.Setup(repository => repository.GetAppointments(It.IsAny<Specialization>())).Returns(() => a);

            var spec = new Specialization(0, "a");
            var res = _service.GetAppointments(spec);

            Assert.True(res.Success);
        }

        [Fact]
        public void EmptyAppointment()
        {
            var app = new Appointment();
            var check = app.IsValid();

            Assert.True(check.Success);
        }

        [Fact]
        public void IvalidPatientID()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, -1, 0);
            var check = app.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect patient ID", check.Error);
        }

        [Fact]
        public void IvalidDoctorID()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, 0, -1);
            var check = app.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect doctor ID", check.Error);
        }

        [Fact]
        public void IvalidTime()
        {
            var app = new Appointment(DateTime.MaxValue, DateTime.MinValue, 0, 0);
            var check = app.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect time provided", check.Error);
        }

        [Fact]
        public void ValidAppintment()
        {
            var app = new Appointment(DateTime.Now, DateTime.Now, 0, 0);
            var check = app.IsValid();

            Assert.True(check.Success);
        }
    }
}