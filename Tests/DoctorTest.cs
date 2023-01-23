using Domain.Logic;
using Domain.Models;
using Domain.UseCases;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Tests
{
    public class DoctorTest
    {
        private readonly DoctorInteractor _service;
        private readonly Mock<IDoctorRepository> _mock;
        private readonly Mock<IAppointmentRepository> __mock;

        public DoctorTest()
        {
            _mock = new Mock<IDoctorRepository>();
            __mock = new Mock<IAppointmentRepository>();
            _service = new DoctorInteractor(_mock.Object, __mock.Object);
        }

        [Fact]
        public void Create_Invalid()
        {
            var doctor = new Doctor();
            var result = _service.CreateDoctor(doctor);

            Assert.True(result.isFailure);
            Assert.Contains("Incorrect doctor", result.Error);
        }

        [Fact]
        public void Create_CreateError()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => new Doctor(-1, "a", -1));
            var doctor = new Doctor(0, "a", 1);
            var result = _service.CreateDoctor(doctor);

            Assert.True(result.isFailure);
            Assert.Contains("Cannot create doctor", result.Error);
        }

        [Fact]
        public void Create_Valid()
        {
            var doctor = new Doctor(0, "a", 1);
            _mock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => doctor);
            var result = _service.CreateDoctor(doctor);

            Assert.True(result.Success);
        }

        [Fact]
        public void Delete_IdNotFound()
        {
            List<Appointment> Appointments = new();

            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Contains("Doctor not found", result.Error);
        }

        [Fact]
        public void Delete_AppointmentsNotEmpty()
        {
            var doctor = new Doctor(0, "a", 1);
            _mock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => doctor);
            __mock.Setup(repository => repository.GetAppointments(doctor.Id)).Returns(() => new List<Appointment>() { new Appointment() });
            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Contains("Cannot delete doctor. Doctor has appointments", result.Error);
        }

        [Fact]
        public void Delete_DoctorNotFound()
        {
            List<Appointment> Appointments = new()
            {
                new Appointment()
            };
            _mock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => null);

            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Contains("Doctor not found", result.Error);
        }

        [Fact]
        public void Delete_DeleteError()
        {
            List<Appointment> Appointments = new();
            _mock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 0));
            _mock.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => new Doctor(-1, "a", 0));

            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Contains("Cannot delete the doctor", result.Error);
        }

        [Fact]
        public void Delete_Valid()
        {
            List<Appointment> Appointments = new();
            _mock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 0));
            _mock.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => new Doctor());

            var result = _service.DeleteDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void GetById_Invalid()
        {
            var result = _service.GetDoctor(-1);

            Assert.True(result.isFailure);
            Assert.Contains("Incorrect doctor id", result.Error);
        }

        [Fact]
        public void GetById_NotFound()
        {
            _mock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => null);

            var result = _service.GetDoctor(0);

            Assert.True(result.isFailure);
            Assert.Contains("Doctor not found", result.Error);
        }

        [Fact]
        public void GetById_Valid()
        {
            _mock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 0));

            var result = _service.GetDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void GetBySpec_Invalid()
        {
            var result = _service.GetDoctor(new Specialization());

            Assert.True(result.isFailure);
            Assert.Contains("Incorrect doctor specialization: ", result.Error);
        }

        [Fact]
        public void GetBySpec_NotFound()
        {
            _mock.Setup(repository => repository.getDoctor(It.IsAny<Specialization>())).Returns(() => null);

            var result = _service.GetDoctor(new Specialization(0, "a"));

            Assert.True(result.isFailure);
            Assert.Contains("Doctor not found", result.Error);
        }

        [Fact]
        public void GetBySpec_Valid()
        {
            var result = _service.GetDoctor(new Specialization());

            Assert.True(result.isFailure);
            Assert.Contains("Incorrect doctor specialization: ", result.Error);
        }

        [Fact]
        public void EmptyDoctor()
        {
            var doctor = new Doctor();
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect doctor id", check.Error);
        }

        [Fact]
        public void NegativeID()
        {
            var doctor = new Doctor(-1, "fullname", 0);
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect doctor id", check.Error);
        }

        [Fact]
        public void EmptyFullname()
        {
            var doctor = new Doctor(1, "", 0);
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect doctor fullname", check.Error);
        }

        [Fact]
        public void InvalidSpecialization()
        {
            var doctor = new Doctor(0, "fullname", 0);
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Contains("Incorrect specialization", check.Error);
        }

        [Fact]
        public void ValidDoctor()
        {
            var doctor = new Doctor(0, "fullname", 0);
            var check = doctor.IsValid();
            Console.WriteLine(check.Error);

            Assert.True(check.Success);
        }
    }
}