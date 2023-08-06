using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Domain.src.Entities;

public class BaseEntity : Timestamp
{
   public Guid Id { get; set;}
}
