﻿using Fiap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Domain.Interfaces
{
    public interface IContato : IRepository<Contato>
    {
        IEnumerable<Contato> GetContatos(int ddd);
    }
}