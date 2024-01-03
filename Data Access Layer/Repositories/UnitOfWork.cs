using Data_Access_Layer.ContextDb;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext _context;
        private Hashtable _repositories;
        public UnitOfWork(UserContext context)
        {
            _context = context;
        }
        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {

                var repositry = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repositry);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
