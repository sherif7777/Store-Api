﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories.Contracts;
using Store.Repository.Data;
using Store.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _storeContext;

		public GenericRepository(StoreContext storeContext)
		{
			_storeContext = storeContext;
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _storeContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _storeContext.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}

		public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		public async Task<int> GetCountAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}

		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec);
		}

		public async Task<T> AddAsync(T Entity)
		{
			var entity = await _storeContext.Set<T>().AddAsync(Entity);

			return entity.Entity;
		}

		public void Update(T Entity)
		{
			_storeContext.Set<T>().Update(Entity);
		}

		public void Delete(T Entity)
		{
			_storeContext.Set<T>().Remove(Entity);
		}
	}

}

