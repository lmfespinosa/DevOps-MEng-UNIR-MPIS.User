#region "Libraires"

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using MPIS.User.AplicationService;
using MPIS.User.EventGrid.Configuration;
using MPIS.User.Function;
using MPIS.User.Function.Configuration;
using MPIS.User.Repository.Configuration;

#endregion

[assembly: FunctionsStartup(typeof(Startup))]
namespace MPIS.User.Function
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddConfiguration();
            
            builder.AddAutomapperService();
            builder.AddPersistenceService();
            builder.AddUserServices();
            builder.AddEventGridService();

        }
    }
}
