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

                    return model;
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

    public class ProductModelCommands
    {
        public class Add : IRequest
        {
            public ProductModel Model { get; set; }

            public class Handler : RequestHandler<Add>
            {
                protected override void Handle(Add request)
                {
                    ModelsRepo.Models.Add(request.Model);
                }
            }
        }

        public class UpdateById : IRequest<(bool Found, string[] errors)>
        {
            public long Id { get; set; }

            public string Name { get; set; }

            public string CatalogDescription { get; set; }

            public class Handler : RequestHandler<UpdateById, (bool Found, string[] errors)>
            {
                protected override (bool Found, string[] errors) Handle(UpdateById request)
                {
                    var model = ModelsRepo.Models.FirstOrDefault(_ => _.Id == request.Id);

                    if (model == null)
                    {
                        return (false, new[] {"Not Found"});
                    }

                    model.Name = request.Name;

                    model.CatalogDescription = request.CatalogDescription;

                    return (true, new string[0]);
                }
            }
        }
    }
}
