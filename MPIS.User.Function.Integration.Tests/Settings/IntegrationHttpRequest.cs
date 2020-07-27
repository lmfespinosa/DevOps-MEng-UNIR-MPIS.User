#region "Libraries"

using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

#endregion

namespace MPIS.User.Function.Integration.Tests.Settings
{
    public static class IntegrationHttpRequest
    {
        public static StringContent CreateContentRequest(object body)
        {
            StringContent content;
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(body);

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            content = new StringContent(json, Encoding.UTF8,
                                    "application/json");

            return content;
        }

        public static string CreateQuery(Guid id)
        {
            string request = string.Empty;
            Dictionary<String, StringValues> query = new Dictionary<string, StringValues>();
            query.Add("Id", id.ToString());


            foreach (KeyValuePair<string, StringValues> keyValues in query)
            {
                request += keyValues.Key + "=" + keyValues.Value + "&";
            }
            return request.TrimEnd('&', ' ');

        }


        public static string CreateQuery(params object[] values)
        {
            string request = string.Empty;


            foreach (KeyValuePair<string, string> keyValues in values)
            {
                request += keyValues.Key + "=" + keyValues.Value + "&";
            }

            return request.TrimEnd('&', ' ');
        }


        public static string CreateQuery(object value)
        {
            string request = string.Empty;

            foreach (var prop in value.GetType().GetProperties())
            {
                request += prop.Name + "=" + prop.GetValue(value).ToString() + "&";
            }


            return request.TrimEnd('&', ' ');
        }
    }
}
