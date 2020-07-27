#region "Libraries"

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace MPIS.User.Function.Unit.Tests.MockServices
{
    public static class MockHttpRequest
    {
        public static Mock<HttpRequest> CreateMockRequest(object body)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(body);

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(ms);

            return mockRequest;
        }

        public static Mock<HttpRequest> CreateMockQuery(Guid body)
        {
            Dictionary<String, StringValues> query = new Dictionary<string, StringValues>();
            query.Add("Id", body.ToString());

            var request = new Mock<HttpRequest>();
            request
                .Setup(x => x.Query)
                .Returns(new QueryCollection(query));


            return request;
        }
    }
}
