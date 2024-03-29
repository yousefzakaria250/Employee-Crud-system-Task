﻿using Data_Access_Layer.ContextDb;
using Data_Access_Layer.Interfaces;
using Db_Builder.Models.User_Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserContext userContext;
        public GenericRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task Add(T entity) => await userContext.Set<T>().AddAsync(entity);

        public async Task<int> Count(ISpecification<T> spec)
         => await ApplySpecification(spec).CountAsync();
        public  T Delete(T entity) =>  userContext.Set<T>().Remove(entity).Entity;

        public async Task<List<T>> GetAllAsync() => await userContext.Set<T>().ToListAsync();

        public async Task<List<T>> GetAllDataWithSpecAsync(ISpecification<T> spec)
      => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await userContext.Set<T>().FindAsync(id);

        public async Task<T> GetByIdAsync(int id, string[]? Includes = null)
        {
            var result = userContext.Set<T>().AsQueryable();
            if (Includes != null)
                foreach (var include in Includes)
                    result = result.Include(include);

            var finalResult = await userContext.Set<T>().FindAsync(id);
            return finalResult;
        }

        public async Task<T> GetByNameAsync(string Name) => await userContext.Set<T>().FindAsync(Name);

        public async Task<T> GetDataByIdWithSpecAsync(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(userContext.Set<T>(), spec);
        }
        public async Task<T> GetData_ByExepressionAsync(Expression<Func<T, bool>> expression) => await userContext.Set<T>().FirstAsync(expression);

        public T Update(T entity) => userContext.Set<T>().Update(entity).Entity;

        public async Task<List<T>> GetData_ByExepressionAsync(Expression<Func<T, bool>> expression, string[]? includes = null)
        {
            IQueryable<T> query = userContext.Set<T>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            return await query.Where(expression).ToListAsync();
        }
    }
}
