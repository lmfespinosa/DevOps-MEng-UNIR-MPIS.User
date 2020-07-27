#region "Libraries"

using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.Function.Integration.Tests.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.Function.Integration.Tests.APIs
{
    public class UserAzureFunctionAPI
    {
        //private HttpServer Server;
        private string UrlBase = "";
        private string x_functions_key = "";
        private HttpClient client;

        public UserAzureFunctionAPI()
        {

            UrlBase = Settings.ConfigurationHelper.Settings.URLFunctionService;/// Environment.GetEnvironmentVariable("URLFunctionService");
            x_functions_key = Settings.ConfigurationHelper.Settings.X_Function_Key;//Environment.GetEnvironmentVariable("X_Function_Key");
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-functions-key", x_functions_key);
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            // GuidAvailable = Guid.Parse("6383ff38-92f9-4bda-e61a-08d783dd9ba7");
        }

        #region "Private Methods"

        public async Task<HttpResponseMessage> CreateUser(CreateUserRequest createUserRequest)
        {
            var content = IntegrationHttpRequest.CreateContentRequest(createUserRequest);
            HttpResponseMessage response = await client.PostAsync(UrlBase + "api/v1/create-user", content);

            return response;
        }


        public async Task<HttpResponseMessage> GetUserById(GetUserByIdRequest getUserByIdRequest)
        {

            //getCompanyRequest.Id = Guid.Parse("6383ff38-92f9-4bda-e61a-08d783dd9ba7");

            var content = IntegrationHttpRequest.CreateQuery(getUserByIdRequest);
            HttpResponseMessage response = await client.GetAsync(UrlBase + String.Format("api/v1/get-User-by-id?{0}", content));


            return response;
        }

        public async Task<HttpResponseMessage> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var content = IntegrationHttpRequest.CreateContentRequest(updateUserRequest);
            HttpResponseMessage response = await client.PutAsync(UrlBase + "api/v1/update-user", content);

            return response;

        }

        public async Task<HttpResponseMessage> DeleteUser(DeleteUserByIdRequest deleteUserRequest)
        {
            var content = IntegrationHttpRequest.CreateQuery(deleteUserRequest);
            HttpResponseMessage response = await client.DeleteAsync(UrlBase + String.Format("api/v1/delete-user?{0}", content));

            return response;
        }


        #endregion
    }
}
