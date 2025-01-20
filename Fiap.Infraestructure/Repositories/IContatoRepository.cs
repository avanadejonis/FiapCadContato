using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Infraestructure.Repositories
{
    public interface IContatoRepository<T> : IRepository<Contato> where T : class
    {
        IEnumerable<Contato> GetByDDD(int ddd);
    }
}