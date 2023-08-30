using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace backend.Domain.src.Entities;

public class Image : BaseEntity
{
    [Url]
    public string ImageUrls { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
