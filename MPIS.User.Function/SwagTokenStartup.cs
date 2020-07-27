#region "Libraries"

using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using MPIS.User.Function;
using System.Reflection;

#endregion

[assembly: WebJobsStartup(typeof(SwagTokenStartup))]
namespace MPIS.User.Function
{
    internal class SwagTokenStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            //Register the extension
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
        }
    }
}
