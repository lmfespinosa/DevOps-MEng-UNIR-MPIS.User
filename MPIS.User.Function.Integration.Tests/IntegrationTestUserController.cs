#region "Libraries"

using Microsoft.AspNetCore.Mvc;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.Function.Integration.Tests.APIs;
using MPIS.User.Function.Integration.Tests.Services;
using MPIS.User.Function.Integration.Tests.Settings;
using MPIS.User.Function.Models.Tests.ComponentValues;
using MPIS.User.Function.Models.Tests.TestCasesSources;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.Function.Integration.Tests
{
    public class IntegrationTestUserController : TestServiceBase
    {
        UserAzureFunctionAPI _userAPI;


        #region "Constructor"

        [SetUp]
        public void Setup()
        {
            _userAPI = new UserAzureFunctionAPI();

            //UserComponentsValues.GuidAvailable = Guid.Parse("897e01cd-6cbf-44d6-4d93-08d81e698e7d");
        }

        #endregion

        #region "Create"

        [Test, Order(1)]
        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.CreateUserTestCases))]
        public async Task TestCreateUserAsync(CreateUserRequest obj, ObjectResult resultAction)
        {
            var request = AutomapperSingleton.Mapper.Map<CreateUserRequest>(obj);

            HttpResponseMessage actionResult = await _userAPI.CreateUser(request);
            if (actionResult.StatusCode == HttpStatusCode.OK)
            {
                dynamic id = JsonConvert.DeserializeObject(actionResult.Content.ReadAsStringAsync().Result, resultAction.Value.GetType());
                UserComponentsValues.GuidAvailable = (Guid)id;
                //RecordComponentsValues.NameAvailable = obj.Name;
            }
            base.CheckAssert(actionResult, resultAction);
        }

        #endregion

        #region "Update"

        [Test, Order(2)]
        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.UpdateUserTestCases))]
        public async Task TestUpdateUser(UpdateUserRequest obj, ObjectResult resultAction, bool elementCreated = false)
        {

            var createdcompanyId = UserComponentsValues.GetUserAviability();

            obj.Id = elementCreated == true ? createdcompanyId : obj.Id;

            HttpResponseMessage actionResult = await _userAPI.UpdateUser(obj);

            base.CheckAssert(actionResult, resultAction);
        }

        #endregion

        #region "Get"

        [Test, Order(3)]
        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.GetUserTestCases))]
        public async Task TestGetUser(GetUserByIdRequest com, ObjectResult resultAction, bool elementCreated = false)
        {
            Guid createdcompanyId = UserComponentsValues.GetUserAviability();

            com.Id = elementCreated == true ? createdcompanyId : com.Id;

            HttpResponseMessage actionResult = await _userAPI.GetUserById(com);

            base.CheckAssert(actionResult, resultAction);
        }

        #endregion

        #region "Delete"

        [Test, Order(4)]
        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.DeleteUserTestCases))]
        public async Task TestDeleteUser(DeleteUserByIdRequest delcom, ObjectResult resultAction, bool elementCreated = false)
        {

            Guid createdtransactionId = UserComponentsValues.GetUserAviability();

            delcom.Id = elementCreated == true ? createdtransactionId : delcom.Id;

            HttpResponseMessage actionResult = await _userAPI.DeleteUser(delcom);

            base.CheckAssert(actionResult, resultAction);
        }

        #endregion
    }
}
