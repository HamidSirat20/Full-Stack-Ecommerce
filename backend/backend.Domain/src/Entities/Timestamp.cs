using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Domain.src.Entities;

public class Timestamp
{
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
