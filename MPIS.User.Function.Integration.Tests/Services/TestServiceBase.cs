#region "Libraries"

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

#endregion

namespace MPIS.User.Function.Integration.Tests.Services
{
    public class TestServiceBase
    {
        public void CheckAssert(HttpResponseMessage actionResult, ObjectResult resultAction)
        {
            try
            {
                switch (actionResult.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                        dynamic value = JsonConvert.DeserializeObject(actionResult.Content.ReadAsStringAsync().Result, resultAction.Value.GetType());

                        this.AssertOk(actionResult, resultAction, value);
                        break;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.NotFound:
                        dynamic valueError = JsonConvert.DeserializeObject(actionResult.Content.ReadAsStringAsync().Result, resultAction.Value.GetType());

                        this.AssertError(actionResult, resultAction, valueError);
                        break;

                    case HttpStatusCode.NoContent:
                        this.AssertNotContent(actionResult, resultAction);
                        break;
                    default:
                        Assert.Fail("Mismatched type");
                        break;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Mismatched type: {ex.Message}");
            }
        }

        private void AssertOk(HttpResponseMessage actionResult, ObjectResult resultAction, dynamic value)
        {
            var status = actionResult.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, status);
            switch (value)
            {
                case Guid g:
                    Assert.IsInstanceOf(resultAction.Value.GetType(), g);
                    Assert.AreNotEqual(g, default(Guid));
                    //IntegrationTestMembershipController.GuidAvailable = (Guid)value;
                    //GuidAvailable = createdId;
                    break;
                case string g:
                    Assert.IsInstanceOf(resultAction.Value.GetType(), g);
                    Assert.AreNotEqual(g, default(string));
                    break;
                case bool b:
                    Assert.IsInstanceOf(resultAction.Value.GetType(), b);
                    Assert.IsTrue(b);
                    break;
                case null:
                    Assert.IsNull(resultAction);
                    break;
                case object o:
                    Assert.IsInstanceOf(resultAction.Value.GetType(), o);
                    break;
            }
        }

        private void AssertError(HttpResponseMessage actionResult, ObjectResult resultAction, dynamic value)
        {
            //var value = JsonConvert.DeserializeObject<object>(actionResult.Content.ReadAsStringAsync().Result);
            var status = actionResult.StatusCode;

            Assert.AreEqual(resultAction.StatusCode, (int)status);
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(value);

            switch (value)
            {
                case IEnumerable<object> ie:
                    CollectionAssert.IsNotEmpty(ie);
                    //CollectionAssert.AllItemsAreInstancesOfType(ie, typeof(List<ValidationFailure>));
                    break;
                case String s:
                    Assert.IsInstanceOf(typeof(String), s);
                    //Assert.AreEqual((string)s, resultContent.message );
                    break;
                case bool s:
                    Assert.IsInstanceOf(typeof(bool), s);
                    //Assert.AreEqual((string)s, resultContent.message );
                    break;
                case Exception e:
                    Assert.IsInstanceOf(typeof(Exception), e);
                    //Assert.AreEqual(e.Message, ((Exception)resultAction.Value).Message);
                    break;
                case object o:
                    Assert.IsNotEmpty(o.ToString());
                    break;
                default:
                    Assert.Fail("Type not identifier");
                    break;
            }
        }

        private void AssertNotContent(HttpResponseMessage actionResult, ObjectResult resultAction)
        {
            //var value = JsonConvert.DeserializeObject<object>(actionResult.Content.ReadAsStringAsync().Result);
            var status = actionResult.StatusCode;

            Assert.AreEqual(resultAction.StatusCode, (int)status);
            Assert.IsNotNull(actionResult);
        }
    }
}
