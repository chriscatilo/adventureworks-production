using AdWorks.Production.Management.WebApi.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdWorks.Production.Management.WebApi.ProductModels
{
    using static ModelsRepo;

    [
        Produces("application/json"), 
        Consumes("application/json"),
        Route("api/[controller]"), 
        ApiController
    ]
    public class ProductsController : ControllerBase
    {

        [HttpGet("models")]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            return Ok(ModelsRepo.Models);
        }

        /// <summary>
        /// Get the product model by Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Product model</returns>
        [HttpGet("models/{id}")]
        public ActionResult<ProductModel> Get(int id)
        {
            var model = ModelsRepo.Models.FirstOrDefault(_ => _.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost("models")]
        public ActionResult Post(ProductModel model)
        {
            ModelsRepo.Models.Add(model);

            return Ok();
        }

        [HttpDelete("/models/{id}")]
        public ActionResult Delete(int id)
        {
            var model = ModelsRepo.Models.FirstOrDefault(_ => _.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            ModelsRepo.Models.Remove(model);

            return Ok();
        }
    }
}
