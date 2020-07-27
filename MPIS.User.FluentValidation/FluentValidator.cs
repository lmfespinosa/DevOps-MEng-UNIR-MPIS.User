#region "Libraries"

using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPIS.Package.HttpMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace MPIS.User.FluentValidation
{
    public partial class FluentValidator
    {

        private const string CONTENT_TYPE_JSON = "application/json";

        protected async Task<IActionResult> Validator<TModel>(HttpRequest request, Type TValidator, Func<TModel, Task<IActionResult>> fun) where TModel : class
        {
            try
            {
                var validator = Activator.CreateInstance(TValidator) as IValidator;

                TModel model = await CheckParameters<TModel>(request);

                var validationResult = validator.Validate(model);

                if (!validationResult.IsValid)
                {
                    //throw ExceptionManager.Create(LayerType.Controller, ExceptionType.Validation, validationResult);
                    throw new Exception(validationResult.ToString());
                }

                return await fun(model);
            }
            catch (Exception sex)
            {
                return new BadRequestObjectResult(sex);
            }
            
        }

        private async Task<TModel> CheckValidatorAsync<TModel>(HttpRequest request, Type TValidator) where TModel : class
        {
            var validator = Activator.CreateInstance(TValidator) as IValidator;

            TModel model = await CheckParameters<TModel>(request);

            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                //throw ExceptionManager.Create(LayerType.Controller, ExceptionType.Validation, validationResult);
            }

            return model;
        }

        private async Task<TModel> CheckParameters<TModel>(HttpRequest request) where TModel : class
        {
            if (request.ContentType == null && request.Query != null && request.Query.Count > 0)
            {
                return HttpMapper.MapQuery<TModel>(request.Query);
            }
            if ((request.ContentType == null || request.ContentType.Contains(CONTENT_TYPE_JSON)) && request.Body != null && request.Body.Length > 0)
            {
                return await HttpMapper.MapStream<TModel>(request.Body);
            }
            
            else
            {
                return null;
            }
        }

    }
}
