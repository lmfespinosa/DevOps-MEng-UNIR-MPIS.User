using AutoMapper;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Baseline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MPIS.Package.HttpMapper;
using MPIS.User.AplicationService.DTOs.User;
using MPIS.User.ApplicationService.Abstract;
using MPIS.User.FluentValidation;
using MPIS.User.FluentValidation.Validation.User;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MPIS.User.Function.Controllers
{
    public partial class UserController : FluentValidator
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;//ServiceFactory.GetUserService();
            _mapper = mapper; 
        }

        [RequestHttpHeader("x-functions-key", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Guid))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Exception))]
        [FunctionName("v1-create-user")]
        public async Task<IActionResult> CreateUserAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/create-user")]
            [RequestBodyType(typeof(CreateUserRequest), "User create")]
            HttpRequest request,
            ILogger logger
            )
        {
            logger.LogInformation($"{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            return await Validator(request, typeof(CreatePostUserRequestValidator), async (CreateUserRequest model) =>
            {
                var response = await _userService.CreateUserAsync(model);

                return new OkObjectResult(response);
            });

            
        }

        [RequestHttpHeader("x-functions-key", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Exception))]
        [FunctionName("v1-update-user")]
        public async Task<IActionResult> UpdateUserAsync(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/update-user")]
            [RequestBodyType(typeof(UpdateUserRequest), "User update")]
            HttpRequest request,
            ILogger logger
            )
        {
            logger.LogInformation($"{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            return await Validator(request, typeof(CreateUpdateUserRequestValidator), async (UpdateUserRequest model) =>
            {
                var response = await _userService.UpdateUserAsync(model);

                return new OkObjectResult(response);
            });
        }

        [RequestHttpHeader("x-functions-key", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Exception))]
        [FunctionName("v1-delete-user")]
        public async Task<IActionResult> DeleteUserAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "v1/delete-user")]
            [RequestBodyType(typeof(DeleteUserByIdRequest), "User delete")]
            HttpRequest request,
            ILogger logger
            )
        {
            logger.LogInformation($"{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            return await Validator(request, typeof(CreateDeleteUserByIdRequestValidator), async (DeleteUserByIdRequest model) =>
            {
                var response = await _userService.DeleteUserByIdAsync(model);

                return new OkObjectResult(response);
            });
        }

        [RequestHttpHeader("x-functions-key", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Exception))]
        [QueryStringParameter("Id", "User Id", DataType = typeof(GetUserByIdRequest))]
        [FunctionName("v1-get-user-by-id")]
        public async Task<IActionResult> GetUserByIdAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/get-user-by-id")]
            HttpRequest request,
            ILogger logger)
           
        {
            logger.LogInformation($"{System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return await Validator(request, typeof(CreateGetUserByIdRequestValidator), async (GetUserByIdRequest model) =>
            {
                var response = await _userService.GetUserByIdAsync(model);

                return new OkObjectResult(response);
            });
        }

        [RequestHttpHeader("x-functions-key", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Exception))]
        //[QueryStringParameter("Id", "User Id", DataType = typeof(GetUserByIdRequest))]
        [FunctionName("v1-get-all-available-user")]
        public async Task<IActionResult> GetAllAvailableUserAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/get-all-available-user")]
            HttpRequest request,
            ILogger logger)

        {
            logger.LogInformation($"{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            var response = await _userService.GetAllAvailableUsersAsync();

            return new OkObjectResult(response);

        }

        [RequestHttpHeader("x-functions-key", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Exception))]
        [FunctionName("v1-is-valid-user-by-email-pass")]
        public async Task<IActionResult> GetIsValidUserByPasswordAsync(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/is-valid-user-by-email-pass")]
            [RequestBodyType(typeof(GetUserByPassEmailRequest), "User update")]
            HttpRequest request,
            ILogger logger
            )
        {
            logger.LogInformation($"{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            return await Validator(request, typeof(CreateGetUserByPassEmailRequestValidator), async (GetUserByPassEmailRequest model) =>
            {
                var response = await _userService.IsValidUserByPasswordAsync(model);

                return new OkObjectResult(response);
            });
        }


    }
}
