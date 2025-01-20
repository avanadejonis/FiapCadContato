using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using Fiap.Infraestructure.Repositories;
using FiapCadContato.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void Get()
        {
            /// Arrange
            var tarefaService = new Mock<IContatoRepository<Contato>>();
            var mock = new Mock<ILogger<ContatosController>>();
            ILogger<ContatosController> logger = mock.Object;
            
            
            tarefaService.Setup(_ => _.Get(1)).Returns(GetContato());
            var sut = new ContatosController(tarefaService.Object, logger);
            /// Act
            var result = sut.Get(1);

            // /// Assert
            //result.StatusCode.Should().Be(200);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDDD()
        {
            /// Arrange
            var tarefaService = new Mock<IContatoRepository<Contato>>();
            var mock = new Mock<ILogger<ContatosController>>();
            ILogger<ContatosController> logger = mock.Object;

            tarefaService.Setup(_ => _.GetByDDD(11)).Returns(GetContatos());
            var sut = new ContatosController(tarefaService.Object, logger);
            /// Act
            var result = sut.GetByDDD(11);

            // /// Assert
            //result.StatusCode.Should().Be(200);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll()
        {
            /// Arrange
            var tarefaService = new Mock<IContatoRepository<Contato>>();
            var mock = new Mock<ILogger<ContatosController>>();
            ILogger<ContatosController> logger = mock.Object;

            tarefaService.Setup(_ => _.GetAll()).Returns(GetContatos());
            var sut = new ContatosController(tarefaService.Object, logger);
            /// Act
            var result = sut.Get();

            // /// Assert
            //result.StatusCode.Should().Be(200);
            Assert.NotNull(result);
        }

        [Fact]
        public void Put()
        {
            /// Arrange
            var contatoService = new Mock<IContatoRepository<Contato>>();
            var mock = new Mock<ILogger<ContatosController>>();
            ILogger<ContatosController> logger = mock.Object;

            contatoService.Setup(_ => _.Update(GetContato())).Returns(1);
            var sut = new ContatosController(contatoService.Object, logger);
            /// Act
            var result = sut.Put(GetContato());

            // /// Assert
            //result.StatusCode.Should().Be(200);
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete()
        {
            /// Arrange
            var contatoService = new Mock<IContatoRepository<Contato>>();
            var mock = new Mock<ILogger<ContatosController>>();
            ILogger<ContatosController> logger = mock.Object;

            contatoService.Setup(_ => _.Delete(GetContato())).Returns(1);
            var sut = new ContatosController(contatoService.Object, logger);
            /// Act
            var result = sut.Delete(1);

            // /// Assert
            Assert.NotNull(result);
        }

        private Contato GetContato()
        {
            return new Contato { Id = 1, Nome = "Joao", Email = "teste1@teste.com", Telefone = 58584343, DDD = 11 };
        }

        private List<Contato> GetContatos()
        {
            var testContatos = new List<Contato>();
            testContatos.Add(new Contato { Id = 1, Nome = "Joao", Email = "teste1@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 2, Nome = "Maria", Email = "teste2@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 3, Nome = "Joaquina", Email = "teste3@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 4, Nome = "Jose", Email = "teste4@teste.com", Telefone = 58584343, DDD = 11 });
            testContatos.Add(new Contato { Id = 5, Nome = "Leila", Email = "teste5@teste.com", Telefone = 58584343, DDD = 11 });


            return testContatos;
        }

    }
}