using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AdWorks.Production.Management.WebApi.Controllers
{

    [
        Produces("application/json"),
        Consumes("application/json"),
        Route("api/products"),
        ApiController
    ]
    public class ModelsController : ControllerBase
    {
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        [HttpPut("models/{id}")]
        public ActionResult Put(int id, [FromBody]Body body)
        {
            //var model = Models.FirstOrDefault(_ => _.Id == id);

            //if (model == null)
            //{
            //    return NotFound();
            //}

            //model.Name = value;

            return Ok();
        }
    }
    public class Body : ValueStore
    {
        public string Name
        {
            get => Get<string>(nameof(Name));
            set => Set(nameof(Name), value);
        }
        public int? Foo
        {
            get => Get<int?>(nameof(Foo));
            set => Set(nameof(Foo), value);
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
