using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    public class Body
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public string Name
        {
            get => _dictionary.TryGetValue(nameof(Name), out var value) ? value : null;
            set => _dictionary[nameof(Name)] = value;
        }
        public int? Foo
        {
            get => _dictionary.TryGetValue(nameof(Foo), out var value) ? int.Parse(value) : default(int?);
            set => _dictionary[nameof(Name)] = value.ToString();
        }
    }
}
