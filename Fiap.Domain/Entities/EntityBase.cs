using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Domain.Entities
{
    public abstract class EntityBase
    {
        [Key]
        [Column("Id",Order = 0)]
        public int Id { get; set; }
    }
}
