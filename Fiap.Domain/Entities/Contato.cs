using Fiap.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Domain.Entities
{
    public class Contato
    {
        [Key]
        [Column("Id", Order = 0)]
        public int Id { get; set; }
        [Column("Nome", Order = 1)]
        public string Nome { get; set; }
        [Column("Email", Order = 2)]
        public string Email { get; set; }
        [Column("Telefone", Order = 3)]
        public int Telefone { get; set; }
        [Column("DDD", Order = 4)]
        public int DDD { get; set; } 
    }
}
