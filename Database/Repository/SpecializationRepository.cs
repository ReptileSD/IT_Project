using Database.Converters;
using Domain.Logic;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repository
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ApplicationContext context;

        public SpecializationRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Specialization Create(Specialization item)
        {
            return context.Add(item.ToModel()).Entity.ToDomain();
        }

        public Specialization? Delete(int id)
        {
            var spec = context.Specializations.FirstOrDefault(s => s.Id == id);
            if (spec == default)
                return null;

            return context.Specializations.Remove(spec).Entity.ToDomain();
        }

        public IEnumerable<Specialization> GetAll()
        {
            return context.Specializations.Select(s => s.ToDomain());
        }

        public Specialization? Get(string name)
        {
            return context.Specializations.FirstOrDefault(s => s.Name == name)?.ToDomain();
        }

        public Specialization? GetItem(int id)
        {
            return context.Specializations.FirstOrDefault(s => s.Id == id)?.ToDomain();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Specialization Update(Specialization item)
        {
            return context.Specializations.Update(item.ToModel()).Entity.ToDomain();
        }
    }
}