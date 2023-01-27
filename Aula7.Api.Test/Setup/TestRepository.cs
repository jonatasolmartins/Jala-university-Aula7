using Aula7.Api.Interfaces;
using System;
using System.Collections.Generic;


namespace Aula7.Api.Test.Setup
{
    public class TestRepository<TInstance> : IRepository<TInstance> where TInstance : class, new()
    {
        private readonly EfContext _context;

        public TestRepository(EfContext context)
        {
            _context = context;
        }
        public void Add(TInstance instance)
        {
            _context.Add(instance);
            _context.SaveChanges();
            Console.WriteLine(instance.GetType().Name);
        }

        public TInstance GetById(int id)
        {
            return  _context.Find<TInstance>(id);
        }

        public List<TInstance> GetAll()
        {
            return new List<TInstance>();
        }
    }
}