using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;

namespace AdWorks.Production.Management.WebApi.ProductModels
{
    [
        Produces("application/json"),
        Consumes("application/json"),
        Route("api/products"),
        DisplayName("Product Models"),
        ApiController
    ]

    public class ModelsPut : ControllerBase
    {
        /// <summary>
        /// Hello There
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// 
        [
            HttpPut("models/{id}"),
            SwaggerOperation(Tags = new []{"Test", "Foo"})
        ]
        public ActionResult Put(int id, Body body)
        {
            var model = ModelsRepo.Models.FirstOrDefault(_ => _.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            var bodyValues = body.ToReadOnlyDictionary();

            var modelProperties = model.GetType().GetProperties()
                .Where(_ => bodyValues.ContainsKey(_.Name));

            foreach (var property in modelProperties)
            {
                dynamic value = bodyValues[property.Name];
                property.SetMethod.Invoke(model, new[] {value});
            }

            return Ok();
        }
    }

    public class Body : ValueStore
    {
        [Description("Product model name.")]
        public string Name
        {
            get => Get<string>(nameof(Name));
            set => Set(nameof(Name), value);
        }

        [Description("Product model catalog description.")]
        public string CatalogDescription
        {
            get => Get<string>(nameof(CatalogDescription));
            set => Set(nameof(CatalogDescription), value);
        }
    }

    public abstract class ValueStore
    {
        private readonly IDictionary<string, dynamic> _dictionary = new ConcurrentDictionary<string, dynamic>();

        protected TValue Get<TValue>(string key)
        {
            return _dictionary.TryGetValue(key, out dynamic value) ? (TValue)value : default(TValue);
        }

        protected void Set<TValue>(string key, TValue value)
        {
            _dictionary[key] = value;
        }

        public IReadOnlyDictionary<string, dynamic> ToReadOnlyDictionary()
        {
            return _dictionary.ToImmutableDictionary();
        }
    }
    
}
