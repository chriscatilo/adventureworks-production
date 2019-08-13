using AdWorks.Production.Management.WebApi.Core.Domain;
using System.Collections.Generic;

namespace AdWorks.Production.Management.WebApi
{
    public static class ModelsRepo
    {
        public static readonly IList<ProductModel> Models = new List<ProductModel>
        {
            new ProductModel()
            {
                Id = 1,
                Name = "Foo",
                CatalogDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
            },
            new ProductModel()
            {
                Id = 2,
                Name = "Bar",
                CatalogDescription = "Morbi ac justo eu mi venenatis tincidunt vel eu urna. Proin mollis sapien at ex efficitur, ut aliquam velit aliquet. Mauris sit amet scelerisque dolor."
            },
            new ProductModel() {
                Id = 3,
                Name = "Qux",
                CatalogDescription = "Proin molestie metus nec eros viverra mollis. Phasellus congue justo justo, varius ornare nisl ultrices elementum."
            }
        };
    }
}