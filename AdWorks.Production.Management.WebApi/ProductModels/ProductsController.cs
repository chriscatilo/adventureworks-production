using AdWorks.Production.Management.WebApi.Core.Domain;
using AdWorks.Production.Management.WebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AdWorks.Production.Management.WebApi.ProductModels
{
    [
        Produces("application/json"), 
        Consumes("application/json"),
        Route("api/[controller]"), 
        ApiController
    ]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("models")]
        public async Task<dynamic> Get()
        {
            var models = await _mediator.Send(new ProductModelQueries.QueryAll());

            return Ok(models);
        }

        /// <summary>
        /// Get the product model by Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Product model</returns>
        [HttpGet("models/{id}")]
        public async Task<dynamic> Get(int id)
        {
            var model = await _mediator.Send(new ProductModelQueries.QueryById {Id = id});

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost("models")]
        public async Task<dynamic> Post(ProductModel model)
        {
            await _mediator.Send(new ProductModelCommands.Add {Model = model});

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
