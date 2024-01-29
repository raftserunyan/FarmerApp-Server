﻿using FarmerApp.Data.DAO;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Data.Repositories
{
    internal class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly FarmerDbContext _context;

        public CommonRepository(FarmerDbContext context)
        {
            _context = context;
        }

        public async Task Add(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddRange(List<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public async Task Delete(int id, DeleteOptions deleteOption = DeleteOptions.Soft)
        {
            var entity = await GetById(id);

            switch (deleteOption)
            {
                case DeleteOptions.Soft:
                    SoftDelete(entity);
                    break;
                case DeleteOptions.Hard:
                    HardDelete(entity);
                    break;
                default:
                    break;
            }
        }

        public void Delete(TEntity entity, DeleteOptions deleteOption = DeleteOptions.Soft)
        {
            switch (deleteOption)
            {
                case DeleteOptions.Soft:
                    SoftDelete(entity);
                    break;
                case DeleteOptions.Hard:
                    HardDelete(entity);
                    break;
                default:
                    break;
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entities, DeleteOptions deleteOption = DeleteOptions.Soft)
        {
            switch (deleteOption)
            {
                case DeleteOptions.Soft:
                    foreach (var entity in entities)
                    {
                        SoftDelete(entity);
                    }
                    break;
                case DeleteOptions.Hard:
                    HardDeleteRange(entities);
                    break;
                default:
                    break;
            }
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<List<TEntity>> GetAll(bool includeDeleted = false)
        {
            var entities = _context.Set<TEntity>().AsQueryable();

            if (includeDeleted)
            {
                entities = entities.IgnoreQueryFilters();
            }

            return await entities.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllBySpecification(ICommonSpecification<TEntity> commonSpecification, bool includeDeleted = false)
        {
            var result = await ApplySpecification(commonSpecification, includeDeleted).ToListAsync();
            return result;
        }

        public async Task<TEntity> GetSingleBySpecification(ICommonSpecification<TEntity> commonSpecification, bool includeDeleted = false)
        {
            var result = ApplySpecification(commonSpecification, includeDeleted);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetById(int id, bool includeDeleted = false)
        {
            var entities = _context.Set<TEntity>().AsQueryable();

            if (includeDeleted)
            {
                entities = entities.IgnoreQueryFilters();
            }

            return await entities.SingleOrDefaultAsync(x => x.Id == id);
        }

        private void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
        }

        private void HardDelete(TEntity entity)
        {
            _context.Remove(entity);
        }

        private void HardDeleteRange(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
        }

        private IQueryable<TEntity> ApplySpecification(ICommonSpecification<TEntity> specification, bool includeDeleted)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }

            query = SpecificationEvaluator<TEntity>.GetQuery(query, specification);

            return query;
        }
    }
}
