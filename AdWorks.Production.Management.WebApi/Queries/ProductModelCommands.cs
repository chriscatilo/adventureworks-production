using System.Linq;
using AdWorks.Production.Management.WebApi.Core.Domain;
using MediatR;

namespace AdWorks.Production.Management.WebApi.Queries
{
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