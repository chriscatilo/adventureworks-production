using AdWorks.Production.Management.WebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace AdWorks.Production.Management.WebApi.ProductModels
{
    using static ProductModelCommands;

    [
        Produces("application/json"),
        Consumes("application/json"),
        Route("api/products"),
        DisplayName("Product Models"),
        ApiController
    ]

    public class ModelsPatch : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModelsPatch(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Hello There
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// 
        [
            HttpPatch("models/{id}")
        ]
        public async Task<dynamic> Patch
        (
            int id, 
            [ModelBinder(BinderType = typeof(CmdBinder), Name = "id")] UpdateById command
        )
        {
            var (found, errors) = await _mediator.Send(command);

            if (!found)
            {
                return NotFound();
            }

            return Ok();
        }
        

        private class CmdBinder : IModelBinder
        {
            private readonly IMediator _mediator;

            public CmdBinder(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task BindModelAsync(ModelBindingContext bindingContext)
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }

                var modelName = bindingContext.ModelName;
                
                var valueProviderResult =
                    bindingContext.ValueProvider.GetValue(modelName);

                var id = long.Parse(valueProviderResult.FirstValue);

                var productModel = await _mediator.Send(new ProductModelQueries.QueryById {Id = id});

                if (productModel == null)
                {
                    return;
                }
             
                var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

                var json = reader.ReadToEnd();

                var body = JsonConvert.DeserializeObject<JObject>(json);

                productModel.Name = body.TryGetValue("name", StringComparison.InvariantCultureIgnoreCase, out var name)
                    ? name.Value<string>()
                    : productModel.Name;

                productModel.CatalogDescription = body.TryGetValue("catalogDescription", StringComparison.InvariantCultureIgnoreCase, out var desc)
                    ? desc.Value<string>()
                    : productModel.CatalogDescription;

                var command = new UpdateById
                {
                    Id = id,
                    Name = productModel.Name,
                    CatalogDescription = productModel.CatalogDescription
                };

                bindingContext.Result = ModelBindingResult.Success(command);
            }
        }
    }
    
}
