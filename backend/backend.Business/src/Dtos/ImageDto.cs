using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos;

public class ImageReadDto
{
    public string ImageUrls { get; set; }
    public Guid ProudctId { get; set; }
}

public class ImageCreateDto
{
    public string ImageUrls { get; set; }

    [ForeignKey(nameof(ProudctId))]
    public Guid ProudctId { get; set; }
}

public class ImageUpdateDto
{
    public string ImageUrls { get; set; }
   public Guid ProudctId { get; set; }
}
