using Domain.Logic;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Domain.UseCases
{
    public class DoctorInteractor
    {
        private readonly IDoctorRepository _db;
        private readonly IAppointmentRepository _apdb;
        public DoctorInteractor(IDoctorRepository db, IAppointmentRepository apdb)
        {
            _db = db;
            _apdb = apdb;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            if (doctor.IsValid().isFailure)
                return Result.Fail<Doctor>("Incorrect doctor: " + doctor.IsValid().Error);
            if (_db.Create(doctor)!.IsValid().Success)
            {
                _db.Save();
                return Result.Ok(doctor);
            }
            return Result.Fail<Doctor>("Cannot create doctor");
        }

        public Result<Doctor> GetDoctor(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Incorrect doctor id");

            var doctor = _db.GetItem(id);
            return doctor != null ? Result.Ok(doctor) : Result.Fail<Doctor>("Doctor not found");
        }
        public Result<IEnumerable<Doctor>> GetDoctor(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.isFailure)
                return Result.Fail<IEnumerable<Doctor>>("Incorrect doctor specialization: " + result.Error);

            var doctors = _db.getDoctor(specialization);

            return doctors != null ? Result.Ok(doctors) : Result.Fail<IEnumerable<Doctor>>("Doctor not found");
        }

        public Result<Doctor> DeleteDoctor(int id)
        {
            var res = _apdb.GetAppointments(id);
            if (res.Any())
                return Result.Fail<Doctor>("Cannot delete doctor. Doctor has appointments");
            var result = GetDoctor(id);
            if (result.isFailure)
                return Result.Fail<Doctor>(result.Error);
            if (_db.Delete(id)!.IsValid().Success)
            {
                _db.Save();
                return result;
            }
            return Result.Fail<Doctor>("Cannot delete the doctor");
        }
        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAll());
        }
    }
}