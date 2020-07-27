#region "Libraries"

using AutoMapper;
using MPIS.User.ApplicationService;
using MPIS.User.ApplicationService.Abstract;
using MPIS.User.Repository;
using MPIS.User.Repository.Abstract;
using MPIS.User.RepositoryModel;

#endregion

namespace MPIS.User.IoC
{
    public static class ServiceFactory
    {

        private static IUserService _userService = null;

        public static IUserService GetUserService()
        {
            if (_userService == null)
                _userService = new UserService();

            return _userService;
        }


        private static IUserRepository _userRepository = null;

        public static IUserRepository GetUserRepository()
        {
            if (_userRepository == null) {
                //Context context = Context();
                _userRepository = new UserRepository();
            }

            return _userRepository;
        }

    }
}
