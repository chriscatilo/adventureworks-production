using AdWorks.Production.Management.WebApi.Core.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace AdWorks.Production.Management.WebApi.Queries
{
    public class ProductModelQueries
    {
        public class QueryById : IRequest<ProductModel>
        {
            public long Id { get; set; }
            
            public class Handler : RequestHandler<QueryById, ProductModel>
            {
                protected override ProductModel Handle(QueryById request)
                {
                    var model = ModelsRepo.Models.FirstOrDefault(_ => _.Id == request.Id);

                    if (model == null)
                    {
                        return null;
                    }

                    return new ProductModel
                    {
                        Id = model.Id,
                        Name = model.Name,
                        CatalogDescription = model.CatalogDescription
                    };
                }
            }
        }

        public class QueryAll : IRequest<IEnumerable<ProductModel>>
        {
            public class Handler : RequestHandler<QueryAll, IEnumerable<ProductModel>>
            {
                protected override IEnumerable<ProductModel> Handle(QueryAll request)
                {
                    return ModelsRepo.Models;
                }
            }
        }
    }
}
