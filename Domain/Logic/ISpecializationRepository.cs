using Domain.Logic;
using Domain.Models;

namespace Domain.Logic
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        public Specialization? Get(string name);
    }
}