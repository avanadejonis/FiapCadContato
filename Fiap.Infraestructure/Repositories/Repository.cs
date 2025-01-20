using Dapper;
using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using Fiap.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private IDbContext _contatoContext;

        public Repository(IDbContext dbContext)
        {
            _contatoContext = dbContext;
        }

        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T? Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
