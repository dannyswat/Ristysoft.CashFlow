using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.ModelBinders
{
    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueResult == ValueProviderResult.None)
                return Task.CompletedTask;

            string serialized = valueResult.FirstValue;

            if (string.IsNullOrEmpty(serialized))
                bindingContext.Result = ModelBindingResult.Success(null);

            try
            {
                bindingContext.Result = ModelBindingResult.Success(
                JsonSerializer.Deserialize(serialized, bindingContext.ModelType));
            }
            catch (Exception e)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            
                return Task.CompletedTask;
        }
    }
}
