using Domain.Models;
using System.Collections.Generic;


namespace Domain.Logic
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable<Doctor>? getDoctor(Specialization specialization);
    }
}