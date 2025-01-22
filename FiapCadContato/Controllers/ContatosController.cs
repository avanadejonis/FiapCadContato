using Fiap.Domain.Entities;
using Fiap.Domain.Interfaces;
using Fiap.Infraestructure.Repositories;
using FiapCadContato.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace FiapCadContato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoRepository<Contato> _contatoService;
        private readonly ILogger<ContatosController> _logger;
        Counter _counter;
        Histogram _histogram;
        Gauge _gauge;
        Summary _summary;


        public ContatosController(IContatoRepository<Contato> contatoRepository, ILogger<ContatosController> logger)
        {
            _contatoService = contatoRepository;
            _logger = logger;
        }

        // GET api/contato
        /// <summary>
        /// Obtem Todos os Contato
        /// </summary>
        /// <returns>Retorna todos Contatos registrados</returns>
        /// <response code="200">Returna os contatos do sistema</response>
        [HttpGet(Name = "GetContatos")]
        public ActionResult<IEnumerable<Contato>> Get()
        {
            try
            {
                _logger.LogInformation("Iniciando a consulta de contatos.");
                var contatos = _contatoService.GetAll();
                if (contatos == null)
                {
                    _logger.LogInformation("Registros não localizados.");
                    return NotFound($"Registros não localizados na consulta.");
                }

                return Ok(contatos);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao tentar obter Contatos: {ex.Message}.");
                throw ex;
            }
        }
        // GET api/contato
        /// <summary>
        /// Obtem Contato por DDD
        /// </summary>
        /// <returns>Retorna o Contato por DDD</returns>
        /// <response code="200">Returna os contatos por DDD</response>
        [HttpGet("{ddd:int}", Name = "GetContatosByDDD/ddd")]
        public ActionResult<IEnumerable<Contato>> GetByDDD(int ddd)
        {
            try
            {
                _logger.LogInformation("Iniciando a consulta de contatos pelo DDD.");
                var contatoValidator = new ContatoValidator();

                if (!contatoValidator.ValidarDDD(ddd))
                {
                    _logger.LogInformation("DDD não pode ser nullo.");
                    return BadRequest("O DDD não pode ser nullo.");
                }

                var contatos = _contatoService.GetByDDD(ddd);
                if (contatos == null)
                {
                    _logger.LogInformation("Contatos não localizados.");
                    return NotFound($"Registros não localizados com o DDD: {ddd}.");
                }

                _logger.LogInformation($"Contatos localizados.{contatos.Count()}");
                return Ok(contatos);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao tentar obter Contatos por DDD: {ex.Message}.");
                throw ex;
            }
        }

        // GET api/contato
        /// <summary>
        /// Obtem Contato
        /// </summary>
        /// <returns>Retorna o Contato por Id</returns>
        /// <response code="200">Returna os contatos por Id</response>
        [HttpGet("GetContato/{id}")]
        //[HttpGet("{id}", Name = "GetContato/id")]
        //[ActionName("GetContato/id")]
        [ActionName(nameof(Get))]
        public ActionResult<Contato> Get(int id)
        {
            _logger.LogInformation("Iniciando a consulta de contatos pelo ID.");

            var contatoValidator = new ContatoValidator();

            if (!contatoValidator.ValidarId(id))
            {
                _logger.LogInformation("Id vazio.");
                return BadRequest("O Id não pode ser nullo.");
            }

            var contato = _contatoService.Get(id);

            if (contato == null)
            {
                _logger.LogInformation($"Contato não localizado com o ID: {id}.");
                return NotFound($"Registro não localizado com o Id: {id}.");
            }

            _logger.LogInformation("Contato localizado com sucesso.");
            return Ok(contato);
        }

        // POST api/Contato
        ///<summary>
        /// Cria um item na tabela do SQL Azure de Contatos.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Contato
        ///     {
        ///        "nome": "nome",
        ///        "email": "email",
        ///        "telefone": "telefone",
        ///        "ddd": "ddd",
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="400">Se o item não for criado</response>
        [HttpPost]
        public ActionResult Post([FromBody] Contato contato)
        {
            _logger.LogInformation("Iniciando a criação do contato.");
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contato);
            if (!result.IsValid)
            {
                _logger.LogInformation("Erros de validação.");
                return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var cratedContato = _contatoService.Create(contato);
            if (cratedContato != null)
            {
                _logger.LogInformation("Contato criado com sucesso.");
                return CreatedAtAction(nameof(Get), new { id = cratedContato.Id }, cratedContato);

            }
            else
            {
                _logger.LogInformation("Erro ao criar contato.");
                return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }


        // PUT api/Contato
        ///<summary>
        /// Atualiza um item na tabela de Contatos.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /Contato
        ///     {
        ///        "id": "id"
        ///        "nome": "nome",
        ///        "email": "email",
        ///        "telefone": "telefone",
        ///        "ddd": "ddd",
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Um item atualizado</returns>
        /// <response code="201">Retorna o item atualizado</response>
        /// <response code="400">Se o item não for atualizado</response>
        [HttpPut]
        public ActionResult Put([FromBody] Contato contato)
        {
            _logger.LogInformation($"Iniciando a atualização do contato pelo ID: {contato.Id}.");

            var contatoValidator = new ContatoValidator();

            if (contatoValidator.ValidarId(contato.Id))
            {
                var result = contatoValidator.Validate(contato);
                if (!result.IsValid)
                {
                    _logger.LogInformation("Erros de validação.");
                    return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
                }

                _contatoService.Update(contato);
                _logger.LogInformation("Contato atualizado com sucesso.");
                return Ok(contato);
            }
            else
            {
                _logger.LogInformation("Contato com ID inválido.");
                return BadRequest("O Id deve conter um valor válido.");
            }
        }

        // DELETE api/contato
        /// <summary>
        /// Deleta Contato
        /// </summary>
        /// <returns>Deleta o Contato pelo Id</returns>
        /// <response code="200">Deleta os contatos pelo Id</response>
        [HttpDelete("{id:int}")]
        public ActionResult<Contato> Delete(int id)
        {
            _logger.LogInformation($"Iniciando exclusão do contato pelo ID: {id}.");

            var contatoValidator = new ContatoValidator();
            if (!contatoValidator.ValidarId(id))
            {
                _logger.LogInformation("ID inválido.");
                return BadRequest("Id não pode ser inválido.");
            }

            var contato = _contatoService.Get(id);
            if (contato == null)
            {
                _logger.LogInformation("Contato não localizado para exclusão.");
                return NotFound("Registro não encontrado.");
            }
            _contatoService.Delete(contato);
            _logger.LogInformation("Contato excluido com sucesso.");
            return Ok(contato);
        }
    }
}
