using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wrench.API.ViewModels.Demandas;
using Wrench.Data.Context;
using Wrench.Domain.Entities;
using Wrench.Domain.Entities.Identity;

namespace Wrench.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DemandasController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly WrenchDbContext _dbContext;

        public DemandasController(UserManager<AppUser> userManager, WrenchDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetDemandasSelecionadas()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            if (await _userManager.IsInRoleAsync(user, "Prestador"))
            {
                return Ok(_dbContext.Set<RegistroServico>().Where(x => (x.Estado == Domain.Enum.EstadoServico.SELECIONADO || x.Estado == Domain.Enum.EstadoServico.TOPADO) && x.IdPrestador == user.Id).Select(x => new
                {
                    x.IdDemanda,
                    x.Estado,
                    x.IdRegistroServico,
                    x.Demanda.Titulo,
                    x.Demanda.Descricao,
                    x.ValorEstimado,
                    x.Prazo,
                    Topada = x.Estado == Domain.Enum.EstadoServico.TOPADO,
                    Demandante = new
                    {
                        x.Demandante.Nome,
                        x.Demandante.Email
                    }
                }).ToList());
            }
            else
            {
                return Ok(_dbContext.Set<Demanda>().Where(x => x.IdElaborador == user.Id && x.Estado == Domain.Enum.EstadoDemanda.DISPONIVEL).Select(x => new
                {
                    x.IdDemanda,
                    x.Titulo,
                    x.Descricao,
                    Topada = x.RegistroServicos.Any(y => y.Estado == Domain.Enum.EstadoServico.TOPADO),
                    Propostas = new[]
                    {
                        x.RegistroServicos.Where(y => y.Estado == Domain.Enum.EstadoServico.SELECIONADO || y.Estado == Domain.Enum.EstadoServico.TOPADO).Select(y => new
                        {
                            y.IdRegistroServico,
                            y.Prazo,
                            y.ValorEstimado,
                            y.Mensagem,
                            Prestador = new
                            {
                                y.Prestador.Nome,
                                y.Prestador.Email
                            }
                        })
                    }
                }).ToList());
            }
        }

        [HttpGet("demandas-abertas")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> GetDemandasDisponiveis()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var demandas_abertas = _dbContext.Set<Demanda>().Where(x => x.Estado == Domain.Enum.EstadoDemanda.DISPONIVEL && x.Tags.Any(y => user.Tags.Contains(y))).Select(x => new
            {
                x.IdDemanda,
                x.IdElaborador,
                x.Titulo,
                x.Descricao,
                Tags = x.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            }).ToList();

            return Ok(demandas_abertas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var demanda = _dbContext.Set<Demanda>().Find(id);

            if (demanda == null)
                return NotFound("Demanda não encontrada.");

            return Ok(new
            {
                demanda.IdDemanda,
                demanda.IdElaborador,
                demanda.Titulo,
                demanda.Descricao,
                Tags = demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            });
        }

        [HttpPost]
        [Authorize(Roles = "Demandante")]
        //1 - Usuário cria uma demanda
        public async Task<ActionResult> CriarDemandaAsync(CriarDemandaViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var tags = _dbContext.Set<Tag>().Where(x => value.Tags.Contains(x.Nome.ToUpper())).ToList();

            var demanda = new Demanda(value.Titulo, value.Descricao, user.Id);

            foreach (var tag in tags)
            {
                demanda.AdicionarTag(tag);
            }

            _dbContext.Set<Demanda>().Add(demanda);

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                demanda.IdDemanda,
                demanda.IdElaborador,
                demanda.Titulo,
                demanda.Descricao,
                Tags = demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            });
        }

        [HttpPost("escolher-demanda")]
        [Authorize(Roles = "Prestador")]
        //2 - Prestador de serviços escolhe uma demanda e envia mensagem
        public async Task<ActionResult> EscolherDemandaAsync(EscolherDemandaViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);

            var tRegistro = _dbContext.Entry(demanda).Collection(x => x.RegistroServicos).LoadAsync();

            if (demanda is null)
                return NotFound("Demanda não encontrada.");

            await tRegistro;

            if (demanda.RegistroServicos.Where(x => x.IdPrestador == user.Id).Any())
                return BadRequest("Essa demanda já foi selecionada pelo usuário atual.");

            demanda.EscolherDemanda(user.Id, value.Valor, value.Prazo, value.Mensagem);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("topar-demanda")]
        [Authorize(Roles = "Demandante")]
        //3 - Usuário topa a proposta de um dos prestadores de serviços
        public async Task<ActionResult> ToparDemandaAsync(ToparDemandaViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);

            var tRegistro = _dbContext.Entry(demanda).Collection(x => x.RegistroServicos).LoadAsync();

            if (demanda is null)
                return NotFound("Demanda não encontrada.");

            await tRegistro;

            demanda.ToparDemanda(value.IdRegistroServico);

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                demanda.IdDemanda,
                demanda.IdElaborador,
                demanda.Titulo,
                demanda.Descricao,
                Tags = demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            });
        }

        //4 - Concluir demanda
        [HttpPost("concluir-demanda")]
        public async Task<ActionResult> ConcluirDemandaAsync(ConcluirDemandaViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);

            var tRegistro = _dbContext.Entry(demanda).Collection(x => x.RegistroServicos).LoadAsync();

            if (demanda is null)
                return NotFound("Demanda não encontrada.");

            await tRegistro;

            var servico = demanda.RegistroServicos.SingleOrDefault(x => x.IdRegistroServico == value.IdRegistroServico);

            if (!servico.CheckDemandante && servico.IdDemandante == user.Id)
                servico.Concluir(RegistroServico.TipoUsuario.DEMANDANTE, value.ValorCobrado);

            if (!servico.CheckPrestador && servico.IdPrestador == user.Id)
                servico.Concluir(RegistroServico.TipoUsuario.PRESTADOR, value.ValorCobrado);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("cancelar-demanda")]
        public async Task<ActionResult> CancelarDemandaAsync(CancelarDemandaViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);

            var tRegistro = _dbContext.Entry(demanda).Collection(x => x.RegistroServicos).LoadAsync();

            if (demanda is null)
                return NotFound("Demanda não encontrada.");

            await tRegistro;

            if (demanda.IdElaborador != user.Id && !demanda.RegistroServicos.Any(y => y.Estado == Domain.Enum.EstadoServico.TOPADO && y.IdPrestador == user.Id))
                return BadRequest("Usuário não pode cancelar esse serviço.");

            demanda.Cancelar();

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("cancelar-servico")]
        public async Task<ActionResult> CancelarServicoAsync(CancelarServicoViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var servico = _dbContext.Set<RegistroServico>().Find(value.IdRegistroServico);

            if (servico is null)
                return NotFound("Serviço não encontrado.");

            if (servico.IdDemandante != user.Id && servico.IdPrestador != user.Id)
                return BadRequest("Usuário não pode cancelar esse serviço.");

            servico.Cancelar();

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("avaliar-servico")]
        public async Task<ActionResult> AvaliarServicoAsync(AvaliarServicoViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var novaAvaliacao = new Avaliacao((Avaliacao.Nota)value.Nota, user.Id);

            var servico = _dbContext.Set<RegistroServico>().Find(value.IdRegistroServico);

            servico.Demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);
            await _dbContext.Entry(servico).Collection(x => x.Avaliacoes).LoadAsync();

            if (servico is null)
                return NotFound("Serviço não encontrado.");

            if (servico.IdDemandante != user.Id && servico.IdPrestador != user.Id)
                return BadRequest("Usuário não pode avaliar este serviço.");

            servico.Avaliar(novaAvaliacao);

            if (servico.EhAvaliado())
            {
                servico.Demanda.Concluir();
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
