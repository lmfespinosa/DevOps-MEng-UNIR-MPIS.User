#region "Libraries"

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MPIS.User.ApplicationService;
using MPIS.User.ApplicationService.Abstract;

#endregion
namespace MPIS.User.AplicationService
{
    public static class IServiceCollectionExtension
    {
        public static void AddUserServices(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            
        }
    }
}
