using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace AdWorks.Production.Management.WebApi.ProductModels
{
    [
        Produces("application/json"),
        Consumes("application/json"),
        Route("api/products"),
        DisplayName("Product Models"),
        ApiController
    ]

    public class ModelsPatch : ControllerBase
    {
        /// <summary>
        /// Hello There
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// 
        [
            HttpPatch("models/{id}")
        ]
        public ActionResult Patch(int id, Body body)
        {
            return Ok();
        }


        [ModelBinder(BinderType = typeof(BodyBinder))]
        public class Body
        {
            public string Name { get; set; }
            
            public string CatalogDescription { get; set; }
        }

        internal class BodyBinder : IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }
                
                var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
                var text = reader.ReadToEnd();

                var body = JsonConvert.DeserializeObject(text);

                return Task.CompletedTask;
            }
        }
    }
    
}
