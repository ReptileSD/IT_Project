﻿using Database.Converters;
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
            var item = GetItem(id);
            if (item == default)
                return null;
            return context.Remove(item.ToModel()).Entity.ToDomain();
        }

        public Specialization? Get(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Specialization> GetAll()
        {
            return context.Specializations.Select(item => item.ToDomain());
        }

        public Specialization? GetItem(int id)
        {
            return context.Specializations.FirstOrDefault(item => item.Id == id)?.ToDomain();
        }
        public void Save()
        {
            context.SaveChangesAsync();
        }

        public Specialization Update(Specialization item)
        {
            return context.Specializations.Update(item.ToModel()).Entity.ToDomain();
        }
    }
}