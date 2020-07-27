#region "Libraries"

using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MPIS.User.AplicationService.DTOs.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;

#endregion

namespace MPIS.User.Function.Unit.Tests.Services
{
    public class TestServiceBase
    {
        public void CheckAssert(IActionResult actionResult, IActionResult resultAction)
        {
            if (actionResult.GetType() == resultAction.GetType() && resultAction is OkObjectResult)
            {
                this.AssertOk(actionResult, resultAction as OkObjectResult);
            }
            else if (actionResult.GetType() == resultAction.GetType() && resultAction is BadRequestObjectResult)
            {
                this.AssertError(actionResult, resultAction as BadRequestObjectResult);
            }
            else if (actionResult is ObjectResult && ((ObjectResult)actionResult).StatusCode == 200 && ((ObjectResult)actionResult).Value is null
                    && resultAction is ObjectResult && ((ObjectResult)resultAction).StatusCode == 204)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Mismatched type");
            }
        }

        private void AssertOk(IActionResult actionResult, ObjectResult resultContent)
        {
            var objectResult = actionResult as ObjectResult;
            var value = objectResult.Value;
            var status = objectResult.StatusCode;

            Assert.AreEqual(status, 200);
            switch (value)
            {
                case Guid g:
                    Assert.IsInstanceOf(resultContent.Value.GetType(), g);
                    Assert.AreNotEqual(g, default(Guid));
                    break;

                case bool b:
                    Assert.IsInstanceOf(resultContent.Value.GetType(), b);
                    Assert.IsTrue(b);
                    break;

                case null:
                    Assert.IsNull(resultContent.Value);
                    break;

                case UserResponse u:
                    Assert.IsInstanceOf(resultContent.Value.GetType(), u);
                    Assert.AreNotEqual(u.Id, default(Guid));
                    break;
                case object o:
                    Assert.IsInstanceOf(resultContent.Value.GetType(), o);
                    break;
            }
        }

        private void AssertError(IActionResult actionResult, BadRequestObjectResult resultContent)
        {
            var objectResult = actionResult as BadRequestObjectResult;
            var value = objectResult.Value;
            var status = objectResult.StatusCode;

            Assert.AreEqual(status, 400);
            Assert.IsNotNull(objectResult);
            Assert.IsNotNull(objectResult.Value);

            switch (value)
            {
                case IEnumerable<object> ie:
                    CollectionAssert.IsNotEmpty(ie);
                    CollectionAssert.AllItemsAreInstancesOfType(ie, typeof(ValidationFailure));
                    break;

                case Exception e:
                    Assert.IsInstanceOf(resultContent.Value.GetType(), e);
                    break;

                case String s:
                    Assert.IsInstanceOf(resultContent.Value.GetType(), s);
                    break;

                default:
                    Assert.Fail("Type not identiifier");
                    break;
            }
        }
    }
}
