using Domain.Models;
using System.Collections.Generic;

namespace Domain.Logic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }


}