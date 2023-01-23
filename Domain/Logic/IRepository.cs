using Domain.Models;
using System.Collections.Generic;

namespace Domain.Logic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetItem(int id);
        T Create(T item);
        T Update(T item);
        T? Delete(int id);
        void Save();
    }


}