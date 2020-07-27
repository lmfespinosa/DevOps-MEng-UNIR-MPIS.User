#region "Libraries"

using Microsoft.EntityFrameworkCore;
using MPIS.Package.Base.Abstract;
using MPIS.Package.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace MPIS.Package.Repository
{
    public class Repository : IRepository
    {

        private readonly DbContext _context;

        public Repository(DbContext context) => _context = context;

        void IDisposable.Dispose() => _context.Dispose();

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<Guid> AddAsync<TDomain>(TDomain entity) where TDomain : class, IBaseDomainModel
        {
            entity.Created = DateTime.UtcNow;

            await _context.Set<TDomain>().AddAsync(entity);

            return entity.Id;
        }
        public async Task AddRangeAsync<TDomain>(IEnumerable<TDomain> entities) where TDomain : class, IBaseDomainModel
        {
            entities = entities.Select(x =>
            {
                x.Created = DateTime.UtcNow;
                return x;
            }).ToList();

            await _context.Set<TDomain>().AddRangeAsync(entities);
        }

        public async Task<bool> UpdateAsync<TDomain>(TDomain data) where TDomain : class, IBaseDomainModel
        {
            var entity = await _context.Set<TDomain>().FirstOrDefaultAsync(x => x.Id == data.Id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(data);
                entity.Updated = DateTime.UtcNow;

                return true;
            }

            throw new Exception($"{nameof(TDomain)} with Id {data.Id} not exists.");
        }

        public async Task UpdateRangeAsync<TDomain>(IEnumerable<TDomain> datas) where TDomain : class, IBaseDomainModel
        {
            var entities = await _context.Set<TDomain>().Where(x => datas.Any(y => y.Id == x.Id)).ToListAsync();

            if (entities != null && entities.Any() && entities.Count() == datas.Count())
            {
                datas.ToList().ForEach(x => x.Updated = DateTime.UtcNow);
                _context.UpdateRange(datas);
            }
            else
            {
                throw new Exception($"{nameof(TDomain)} not all exists.");
            }
        }

        public async Task<bool> DeleteAsync<TDomain>(Guid identifier) where TDomain : class, IBaseDomainModel
        {
            var entity = await _context.Set<TDomain>().FindAsync(identifier);

            if (entity != null)
            {
                entity.Deleted = DateTime.UtcNow;

                return true;
            }

            throw new Exception($"{nameof(TDomain)} with Id {identifier} not exists.");
        }

        public async Task DeleteRangeAsync<TDomain>(IEnumerable<Guid> identifiers) where TDomain : class, IBaseDomainModel
        {
            var entities = await _context.Set<TDomain>().Where(x => identifiers.Any(y => y == x.Id)).ToListAsync();

            if (entities != null && entities.Any() && entities.Count == identifiers.Count())
            {
                entities.ForEach(x => x.Deleted = DateTime.UtcNow);
            }
            else
            {
                throw new Exception($"{nameof(TDomain)} not all exists.");
            }
        }


        public async Task<bool> DeleteHardAsync<TDomain>(Guid identifier) where TDomain : class, IBaseDomainModel
        {
            var entity = await _context.Set<TDomain>().FindAsync(identifier);

            if (entity != null)
            {
                _context.Remove(entity);
                return true;
            }

            throw new Exception($"{nameof(TDomain)} with Id {identifier} not exists.");
        }

        public async Task DeleteHardRangeAsync<TDomain>(IEnumerable<Guid> identifiers) where TDomain : class, IBaseDomainModel
        {
            var entities = await _context.Set<TDomain>().Where(x => identifiers.Any(y => y == x.Id)).ToListAsync();

            if (entities != null && entities.Any() && entities.Count == identifiers.Count())
            {
                _context.RemoveRange(entities);
            }
            else
            {
                throw new Exception($"{nameof(TDomain)} not all exists.");
            }
        }

        public async Task<bool> DeleteAsync<TDomain>(Expression<Func<TDomain, bool>> lambda) where TDomain : class, IBaseDomainModel
        {
            var entities = await _context.Set<TDomain>().Where(lambda).ToListAsync();

            if (entities != null)
            {
                entities.ForEach(x => x.Deleted = DateTime.UtcNow);

                return true;
            }

            throw new Exception($"Any {nameof(TDomain)} has been found with specified expression.");
        }

        public async Task<bool> DeleteHardAsync<TDomain>(Expression<Func<TDomain, bool>> lambda) where TDomain : class, IBaseDomainModel
        {
            var entities = await _context.Set<TDomain>().Where(lambda).ToListAsync();

            if (entities != null)
            {
                _context.RemoveRange(entities);

                return true;
            }

            throw new Exception($"Any {nameof(TDomain)} has been found with specified expression.");
        }

        public async Task<bool> ExistsAsync<TDomain>(Expression<Func<TDomain, bool>> lambda) where TDomain : class, IBaseDomainModel
        {
            return await _context.Set<TDomain>().AnyAsync(lambda);
        }

        public IQueryable<TDomain> GetAsync<TDomain>(Expression<Func<TDomain, bool>> lambda = null) where TDomain : class, IBaseDomainModel
        {
            return lambda == null
                ? _context.Set<TDomain>().AsQueryable()
                : _context.Set<TDomain>().Where(lambda);
        }

        public async Task<TDomain> FindAsync<TDomain>(Guid id) where TDomain : class, IBaseDomainModel
        {
            return await _context.Set<TDomain>().FindAsync(id);
        }
    }
}
