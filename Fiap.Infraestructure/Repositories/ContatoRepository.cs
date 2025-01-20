using Dapper;
using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using Fiap.Infraestructure.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Infraestructure.Repositories
{
    public class ContatoRepository<T> : IContatoRepository<T> where T : class
    {
        private IDbContext _contatoContext;

        public ContatoRepository(IDbContext dbContext)
        {
            _contatoContext = dbContext;
        }

        public Contato Create(Contato entity)
        {         
            string query = @"INSERT INTO dbo.[Contato](Nome,Email,Telefone,DDD)
                        OUTPUT INSERTED.*
                        VALUES(@Nome,@Email,@Telefone,@DDD);";
            
            using var connection = _contatoContext.CreateConnection();
            var result = connection.QuerySingle<Contato>(query, entity);

            return result;
        }

        public int Delete(Contato entity)
        {
            var query = "Delete From Contato Where Id = @Id";

            using var connection = _contatoContext.CreateConnection();
            var result = connection.Execute(query, entity);

            return result;
        }

        public Contato? Get(Expression<Func<Contato, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Contato? Get(int Id)
        {
            string query = $"SELECT Id, Nome, Email, Telefone, DDD FROM Contato WHERE id = '{Id}'";

            using var connection = _contatoContext.CreateConnection();

            var contato = connection.Query<Contato>(query).FirstOrDefault();
            return contato;
        }

        public IEnumerable<Contato> GetAll()
        {
            string query = $"SELECT Id, Nome, Email, Telefone, DDD FROM Contato";

            using var connection = _contatoContext.CreateConnection();

            var contato = connection.Query<Contato>(query);
            return contato;
        }

        public IEnumerable<Contato> GetByDDD(int ddd)
        {
            string query = $"SELECT Id, Nome, Email, Telefone, DDD FROM Contato Where ddd = '{ddd}'";

            using var connection = _contatoContext.CreateConnection();

            var contato = connection.Query<Contato>(query);
            return contato;
        }

        public IEnumerable<Contato> GetContatos(int ddd)
        {
            throw new NotImplementedException();
        }

        public int Update(Contato entity)
        {
            var query = @"UPDATE Contato SET Nome = @Nome, Email = @Email, Telefone = @Telefone, DDD =
                           @DDD WHERE Id = @Id; ";

            using var connection = _contatoContext.CreateConnection();

            var result = connection.Execute(query, entity);
            return result;
        }
    }
}
