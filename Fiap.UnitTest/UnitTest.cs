using Fiap.Domain.Entities;
using Fiap.Infraestructure.Repositories;
using FiapCadContato.Controllers;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;

namespace Fiap.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        private Contato GetContato()
        {
            return new Contato { Id = 1, Nome = "Joao", Email = "teste1@teste.com", Telefone = 58584343, DDD = 11 };
        }

        private List<Contato> GetTestContatos()
        {
            var testContatos = new List<Contato>();
            testContatos.Add(new Contato { Id = 1, Nome = "Joao", Email = "teste1@teste.com",Telefone=58584343, DDD =11});
            testContatos.Add(new Contato { Id = 2, Nome = "Maria", Email = "teste2@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 3, Nome = "Joaquina", Email = "teste3@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 4, Nome = "Jose", Email = "teste4@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 5, Nome = "Leila", Email = "teste5@teste.com", Telefone = 58584343, DDD = 11 });


            return testContatos;
        }

    }
}