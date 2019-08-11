using System.ComponentModel.DataAnnotations;

namespace AdWorks.Production.Management.WebApi.Core.Domain
{
    public class ProductModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
