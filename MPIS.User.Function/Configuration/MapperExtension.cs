#region "Libraries"

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Reflection;
using MPIS.User.AutoMapper.Profiles;
using AutoMapper;

#endregion

namespace MPIS.User.Function.Configuration
{
    public static class MapperExtension
    {
        public static void AddAutomapperService(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));
        }
    }
}
