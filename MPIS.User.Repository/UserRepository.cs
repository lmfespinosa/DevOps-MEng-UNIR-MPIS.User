#region "Libraries"

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.Repository.Abstract;
using MPIS.User.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.Repository
{
    public class UserRepository :  Package.Repository.Repository, IUserRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(Context context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateUserAsync(CreateUserRequest request)
        {
            var id = await AddAsync(_mapper.Map<User.DomainModel.User>(request));
            await SaveAsync();

            return id;
        }

        public async Task<bool> DeleteUserByIdAsync(DeleteUserByIdRequest request)
        {
            var result = await DeleteAsync<User.DomainModel.User>(request.Id);
            await SaveAsync();

            return result;
        }

        public async Task<List<UserResponse>> GetAllAvailableUsersAsync()
        {
            return await GetAsync<User.DomainModel.User > ()
               .Where(x => !x.Deleted.HasValue && x.IsActive())
               .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
               .ToListAsync();
        }

        public async Task<UserResponse> GetUserByIdAsync(GetUserByIdRequest request)
        {
            return await GetAsync<User.DomainModel.User>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsValidUserByPasswordAsync(GetUserByPassEmailRequest request)
        {
            var list = await GetAsync<User.DomainModel.User>()
                .Where(x => x.Email == request.Email && x.Password == request.Password)
                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return list.Count > 0;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var result = await UpdateAsync(_mapper.Map<User.DomainModel.User>(request));
            await SaveAsync();

            return result;
        }
    }
}
