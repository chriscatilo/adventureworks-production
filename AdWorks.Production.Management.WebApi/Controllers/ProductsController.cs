using AdWorks.Production.Management.WebApi.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdWorks.Production.Management.WebApi.Controllers
{

    [
        Produces("application/json"), 
        Consumes("application/json"),
        Route("api/[controller]"), 
        ApiController
    ]
    public partial class ProductsController : ControllerBase
    {
        private static readonly IList<ProductModel> Models = new List<ProductModel>
        {
            new ProductModel() { Id = 1, Name = "Foo" },
            new ProductModel() { Id = 2, Name = "Bar" },
            new ProductModel() { Id = 3, Name = "Qux" }
        };


        [HttpGet("models")]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            return Ok(Models);
        }

        /// <summary>
        /// Get the product model by Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Product model</returns>
        [HttpGet("models/{id}")]
        public ActionResult<ProductModel> Get(int id)
        {
            var model = Models.FirstOrDefault(_ => _.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost("models")]
        public ActionResult Post(ProductModel model)
        {
            Models.Add(model);

            return Ok();
        }

        [HttpDelete("/models/{id}")]
        public ActionResult Delete(int id)
        {
            var model = Models.FirstOrDefault(_ => _.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            Models.Remove(model);

            return Ok();
        }
    }
}
