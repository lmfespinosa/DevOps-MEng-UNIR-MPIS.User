#region "Libraries"

using MPIS.Package.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace MPIS.Package.Repository.Abstract
{
    public interface IRepository : IDisposable
    {
        Task<bool> SaveAsync();
        Task<Guid> AddAsync<TDomain>(TDomain entity) where TDomain : class, IBaseDomainModel;
        Task AddRangeAsync<TDomain>(IEnumerable<TDomain> entity) where TDomain : class, IBaseDomainModel;

        Task<bool> UpdateAsync<TDomain>(TDomain data) where TDomain : class, IBaseDomainModel;
        Task UpdateRangeAsync<TDomain>(IEnumerable<TDomain> data) where TDomain : class, IBaseDomainModel;

        Task<bool> DeleteAsync<TDomain>(Guid identifier) where TDomain : class, IBaseDomainModel;
        Task DeleteRangeAsync<TDomain>(IEnumerable<Guid> identifier) where TDomain : class, IBaseDomainModel;

        Task<bool> DeleteHardAsync<TDomain>(Guid identifier) where TDomain : class, IBaseDomainModel;
        Task DeleteHardRangeAsync<TDomain>(IEnumerable<Guid> identifier) where TDomain : class, IBaseDomainModel;

        Task<bool> DeleteAsync<TDomain>(Expression<Func<TDomain, bool>> lambda) where TDomain : class, IBaseDomainModel;
        Task<bool> DeleteHardAsync<TDomain>(Expression<Func<TDomain, bool>> lambda) where TDomain : class, IBaseDomainModel;
        Task<bool> ExistsAsync<TDomain>(Expression<Func<TDomain, bool>> lambda) where TDomain : class, IBaseDomainModel;
        IQueryable<TDomain> GetAsync<TDomain>(Expression<Func<TDomain, bool>> lambda = null) where TDomain : class, IBaseDomainModel;
        Task<TDomain> FindAsync<TDomain>(Guid id) where TDomain : class, IBaseDomainModel;
    }
}
