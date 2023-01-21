using Domain.Models;
using System.Collections.Generic;


namespace Domain.Logic
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool createDoctor(Doctor doctor);
        bool deleteDoctor(int id);
        IEnumerable<Doctor> GelAllDoctors();
        Doctor? getDoctor(int id);
        Doctor? getDoctor(Specialization specialization);
    }
}