#region "Libraries"

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.ApplicationService;
using MPIS.User.EventGrid.Abstract;
using MPIS.User.Function.Controllers;
using MPIS.User.Function.Models.Tests.ComponentValues;
using MPIS.User.Function.Models.Tests.TestCasesSources;
using MPIS.User.Function.Unit.Tests.MockServices;
using MPIS.User.Function.Unit.Tests.Services;
using MPIS.User.Function.Unit.Tests.Settings;
using MPIS.User.Repository;
using MPIS.User.RepositoryModel;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.Function.Unit.Tests.ControllerTests
{
    [TestFixture]
    public partial class UnitTestUserController : TestServiceBase 
    {
        #region "Atributes"

        private ILogger _logger;
        private IUserPublisher _userPublisher;

        #endregion

        #region "Constructor"

        [SetUp]
        public void Setup()
        {
            _logger = TestLogger.Create<ILogger>();
            _userPublisher = MockUserPublisher.Create();
            
        }

        #endregion

        #region "Create"

        [Test]
        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.CreateUserTestCases))]
        public async Task TestCreateUser(CreateUserRequest obj, IActionResult resultAction)
        {
            //var request = AutomapperSingleton.Mapper.Map<CreateCompanyRequest>(obj);

            IActionResult actionResult = await CreateUser(obj);

            var objectResult = actionResult as ObjectResult;
            if (objectResult.StatusCode == 200)
            {
                dynamic id = Guid.Parse(objectResult.Value.ToString());
                UserComponentsValues.GuidAvailable = (Guid)id;
                //RecordComponentsValues.NameAvailable = obj.Name;
            }

            base.CheckAssert(actionResult, resultAction);
        }

        #endregion

        #region "Update"

        [Test]
        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.UpdateUserTestCases))]
        public async Task TestUpdateUser(UpdateUserRequest obj, ObjectResult resultAction, bool elementCreated = false)
        {
            UserResponse defaultCompany = await this.CreatedDefaultUser();

            obj.Id = elementCreated == true ? defaultCompany.Id : obj.Id;

            IActionResult actionResult = await this.UpdateUser(obj);

            base.CheckAssert(actionResult, resultAction);
        }
        
        #endregion

        #region "Get"

        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.GetUserTestCases))]
        public async Task TestGetNotification(GetUserByIdRequest getcom, ObjectResult resultAction, bool elementCreated = false)
        {
            UserResponse defaultNotification = await this.CreatedDefaultUser();

            getcom.Id = elementCreated == true ? defaultNotification.Id : getcom.Id;

            IActionResult actionResult = await this.GetUserById(getcom);

            base.CheckAssert(actionResult, resultAction);
        }

        #endregion


        #region "Delete"

        [TestCaseSource(typeof(TestCaseSourcesUser), nameof(TestCaseSourcesUser.DeleteUserTestCases))]
        public async Task TestDeleteNotification(DeleteUserByIdRequest delnot, ObjectResult resultAction, bool elementCreated = false)
        {
            UserResponse defaultNotification = await this.CreatedDefaultUser();

            //Guid createdtransactionId = CompanyComponentsValues.GetCompanyAviability();

            delnot.Id = elementCreated == true ? defaultNotification.Id : delnot.Id;

            IActionResult actionResult = await this.DeleteUser(delnot);

            base.CheckAssert(actionResult, resultAction);
        }

        #endregion


        #region "Private Methods"

        private async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "User")
                .Options;
            

            // Run the test against one instance of the context
            using (var context = new Context(options))
            {

                var repository = new UserRepository(context, AutomapperSingleton.Mapper);
                var service = new UserService(repository, AutomapperSingleton.Mapper,_userPublisher);
                var controller = new UserController(service, AutomapperSingleton.Mapper);



                Mock<HttpRequest> mockCreateRequest = MockHttpRequest.CreateMockRequest(createUserRequest);
                return await controller.CreateUserAsync(mockCreateRequest.Object, _logger); //as GridController;

            }
        }

        private async Task<IActionResult> DeleteUser(DeleteUserByIdRequest deleteUserRequest)
        {

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "User")
                .Options;

          
            // Run the test against one instance of the context
            using (var context = new Context(options))
            {

                var repository = new UserRepository(context, AutomapperSingleton.Mapper);
                var service = new UserService(repository, AutomapperSingleton.Mapper, _userPublisher);
                var controller = new UserController(service, AutomapperSingleton.Mapper);

                Mock<HttpRequest> mockCreateRequest = MockHttpRequest.CreateMockQuery(deleteUserRequest.Id);
                return await controller.DeleteUserAsync(mockCreateRequest.Object, _logger); //as GridController;

            }
        }

        private async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest)
        {

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "User")
                .Options;

            

            // Run the test against one instance of the context
            using (var context = new Context(options))
            {
                var repository = new UserRepository(context, AutomapperSingleton.Mapper);
                var service = new UserService(repository, AutomapperSingleton.Mapper, _userPublisher);
                var controller = new UserController(service, AutomapperSingleton.Mapper);

                Mock<HttpRequest> mockCreateRequest = MockHttpRequest.CreateMockRequest(updateUserRequest);
                return await controller.UpdateUserAsync(mockCreateRequest.Object, _logger); //as GridController;

            }
        }

        private async Task<IActionResult> GetUserById(GetUserByIdRequest getUserRequest)
        {

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "User")
                .Options;


            // Run the test against one instance of the context
            using (var context = new Context(options))
            {
                var repository = new UserRepository(context, AutomapperSingleton.Mapper);
                var service = new UserService(repository, AutomapperSingleton.Mapper, _userPublisher);
                var controller = new UserController(service, AutomapperSingleton.Mapper);

                Mock<HttpRequest> mockGetRequest = MockHttpRequest.CreateMockQuery(getUserRequest.Id);
                return await controller.GetUserByIdAsync(mockGetRequest.Object, _logger); //as GridController;

            }
        }

        private async Task<UserResponse> CreatedDefaultUser()
        {
            CreateUserRequest user = UserComponentsValues.CreateUserRequestBasic();
            var request = AutomapperSingleton.Mapper.Map<UserResponse>(user);

            IActionResult actionResult = await CreateUser(user);

            ObjectResult objectResult = actionResult is ObjectResult ? actionResult as ObjectResult : null;
            if (objectResult != null && objectResult.Value is Guid)
            {
                var identifier = objectResult.Value as Guid?;

                if (identifier.HasValue)
                {
                    request.Id = identifier.Value;
                }
                else
                {
                    Assert.Fail("Return value isn't a identifier valid");
                }
            }
            else
            {
                Assert.Fail("Imposible create default record");
            }

            return request;
        }
        #endregion
    }
}
