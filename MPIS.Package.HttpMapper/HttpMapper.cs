using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MPIS.Package.HttpMapper
{
    public static class HttpMapper
    {
        public static T MapQuery<T>(IQueryCollection parameters) where T : class
        {
            var instance = Activator.CreateInstance<T>();

            foreach (var property in GetProperties<T>(instance))
            {
                foreach (var parameter in GetParameters(parameters))
                {
                    if (Converter.SnakeToCamel(parameter.Key) == Converter.SnakeToCamel(property.Name))
                    {
                        CheckValue(property, instance, parameter.Value);
                    }
                }
            }

            return instance;
        }

        public static T MapFormData<T>(IFormCollection parameters, IFormCollection formfileCollection) where T : class
        {
            var instance = Activator.CreateInstance<T>();

            foreach (var property in GetProperties<T>(instance))
            {
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        if (Converter.SnakeToCamel(parameter.Key) == Converter.SnakeToCamel(property.Name))
                        {
                            CheckValue(property, instance, parameter.Value);
                        }
                    }
                }

                if (formfileCollection?.Files?.Count > 0)
                {
                    if (property.PropertyType == typeof(IFormFileCollection))
                    {
                        property.SetValue(instance, formfileCollection.Files);
                    }
                    else
                    {
                        foreach (var parameter in formfileCollection.Files)
                        {
                            if (Converter.SnakeToCamel(parameter.Name) == Converter.SnakeToCamel(property.Name))
                            {
                                // IFormFile
                                if (property.PropertyType == typeof(IFormFileCollection))
                                    property.SetValue(instance, parameter);
                            }
                        }
                    }
                }
            }

            return instance;
        }

        private static void CheckValue<T>(PropertyInfo property, T instance, StringValues value) where T : class
        {
            // Guid
            if (property.PropertyType == typeof(Guid))
                property.SetValue(instance, Default.GetDefaultGuidOrValue(value));

            if (property.PropertyType == typeof(Guid?))
                property.SetValue(instance, Default.GetDefaultGuidNullableOrValue(value));

            // DateTime
            if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                property.SetValue(instance, Convert.ToDateTime(value));

            // String
            if (property.PropertyType == typeof(string))
                property.SetValue(instance, value.ToString());

            // Int
            if (property.PropertyType == typeof(int))
                property.SetValue(instance, Default.GetDefaultIntOrValue(value));

            if (property.PropertyType == typeof(int?) && string.IsNullOrEmpty(value))
                property.SetValue(instance, Default.GetDefaultIntNullableOrValue(value));

            // Decimal
            if (property.PropertyType == typeof(decimal))
                property.SetValue(instance, Default.GetDefaultDecimalOrValue(value));

            if (property.PropertyType == typeof(decimal?) && string.IsNullOrEmpty(value))
                property.SetValue(instance, Default.GetDefaultDecimalNullableOrValue(value));

            // Long
            if (property.PropertyType == typeof(long))
                property.SetValue(instance, Default.GetDefaultLongOrValue(value));

            if (property.PropertyType == typeof(long?) && string.IsNullOrEmpty(value))
                property.SetValue(instance, Default.GetDefaultLongNullableOrValue(value));

            // Bool
            if (property.PropertyType == typeof(bool))
                property.SetValue(instance, Default.GetDefaultBooleanOrValue(value));

            if (property.PropertyType == typeof(bool?) && string.IsNullOrEmpty(value))
                property.SetValue(instance, Default.GetDefaultBooleanNullableOrValue(value));

            // Double
            if (property.PropertyType == typeof(double))
                property.SetValue(instance, Default.GetDefaultDoubleOrValue(value));

            if (property.PropertyType == typeof(double?) && string.IsNullOrEmpty(value))
                property.SetValue(instance, Default.GetDefaultDoubleNullableOrValue(value));

            // Enum
            if (property.PropertyType.IsEnum)
                property.SetValue(instance, Enum.Parse(property.PropertyType, value, true));
        }

        public static async Task<T> MapStream<T>(Stream stream)
        {
            T data;
            using (var streamReader = new StreamReader(stream))
            {
                var @string = await streamReader.ReadToEndAsync();
                data = JsonConvert.DeserializeObject<T>(@string);
            }
            return data;
        }

        private static IEnumerable<PropertyInfo> GetProperties<T>(T instance)
        {
            var properties = instance.GetType().GetProperties();

            if (!properties.Any()) throw new ArgumentException($"{instance.GetType().Name} does not contain any property.");

            return properties;
        }

        private static IQueryCollection GetParameters(IQueryCollection parameters)
        {
            if (!parameters.Any()) throw new ArgumentException($"{parameters.GetType().Name} does not contain any parameter.");

            return parameters;
        }

        private static IFormCollection GetParameters(IFormCollection parameters)
        {
            if (!parameters.Any()) throw new ArgumentException($"{parameters.GetType().Name} does not contain any parameter.");

            return parameters;
        }

        private static IFormFileCollection GetParameters(IFormFileCollection parameters)
        {
            if (!parameters.Any()) throw new ArgumentException($"{parameters.GetType().Name} does not contain any parameter.");

            return parameters;
        }

        private static bool HasValue(KeyValuePair<string, StringValues> parameter) => parameter.Value != string.Empty;
    }
}
