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
                    Tags = x.Demanda.Tags.Select(y => new
                    {
                        y.IdTag,
                        y.Nome
                    }),
                    Demandante = new
                    {
                        x.Demandante.Nome,
                        x.Demandante.Email
                    }
                }).ToList());
            }
            else
            {
                return Ok(_dbContext.Set<Demanda>().Where(x => x.IdElaborador == user.Id && (x.Estado == Domain.Enum.EstadoDemanda.DISPONIVEL || x.Estado == Domain.Enum.EstadoDemanda.EXECUCAO)).Select(x => new
                {
                    x.IdDemanda,
                    x.Titulo,
                    x.Descricao,
                    Tags = x.Tags.Select(y => new
                    {
                        y.IdTag,
                        y.Nome
                    }),
                    Topada = x.RegistroServicos.Any(y => y.Estado == Domain.Enum.EstadoServico.TOPADO),
                    Propostas = x.RegistroServicos.Any(y => y.Estado == Domain.Enum.EstadoServico.TOPADO) ? new[]
                    {
                        x.RegistroServicos.Where(y => y.Estado == Domain.Enum.EstadoServico.TOPADO).Select(y => new
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
                    } : new[]
                    {
                        x.RegistroServicos.Where(y => y.Estado == Domain.Enum.EstadoServico.SELECIONADO).Select(y => new
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

        [HttpGet("demandas-escolhidas")]
        public async Task<ActionResult> GetDemandasEscolhidas()
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

            var demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);

            await _dbContext.Entry(demanda).Collection(x => x.RegistroServicos).LoadAsync();

            var servico = demanda.RegistroServicos.FirstOrDefault(x => x.Estado == Domain.Enum.EstadoServico.CONCLUIDO);

            if (servico is null)
                return NotFound("Serviço não encontrado.");

            if (servico.IdDemandante != user.Id && servico.IdPrestador != user.Id)
                return BadRequest("Usuário não pode avaliar este serviço.");

            await _dbContext.Entry(servico).Collection(x => x.Avaliacoes).LoadAsync();

            servico.Avaliar(novaAvaliacao);

            if (servico.EhAvaliado())
            {
                servico.Demanda.Concluir();
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        #region Safezone
        [HttpGet("minhas-demandas")]
        [Authorize(Roles = "Demandante")]
        public async Task<ActionResult> GetMinhasDemandas()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(_dbContext.Set<Demanda>().Where(x => x.IdElaborador == user.Id && (x.Estado == Domain.Enum.EstadoDemanda.DISPONIVEL || x.Estado == Domain.Enum.EstadoDemanda.EXECUCAO)).Select(x => new
            {
                x.IdDemanda,
                x.Titulo,
                x.Descricao,
                x.Estado,
                DeveAvaliar = x.RegistroServicos.Any(y => y.Estado == Domain.Enum.EstadoServico.CONCLUIDO && !y.Avaliacoes.Any(z => z.IdUsuario == user.Id)),
                PodeCancelar = x.Estado == Domain.Enum.EstadoDemanda.DISPONIVEL || !x.RegistroServicos.Any(x => x.Estado == Domain.Enum.EstadoServico.TOPADO || x.Estado == Domain.Enum.EstadoServico.CONCLUIDO),
                Tags = x.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            }));
        }

        [HttpGet("demandas-topadas")]
        [Authorize(Roles = "Demandante")]
        public async Task<ActionResult> GetMinhasDemandasTopadas()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            var demandasTopadas = _dbContext.Set<RegistroServico>().Where(x => x.Estado == Domain.Enum.EstadoServico.TOPADO && x.IdDemandante == user.Id);

            return Ok(demandasTopadas.Select(x => new
            {
                x.IdDemanda,
                x.IdRegistroServico,
                x.Demanda.Titulo,
                x.Prazo,
                x.ValorEstimado,
                Prestador = new { x.Prestador.Id, x.Prestador.Email }
            }));
        }

        [HttpGet("buscar-propostas")]
        [Authorize(Roles = "Demandante")]
        public async Task<ActionResult> GetPropostas()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");
            var propostas = _dbContext.Set<RegistroServico>().Where(x => x.Estado == Domain.Enum.EstadoServico.SELECIONADO && x.IdDemandante == user.Id).Select(x => new
            {
                x.Demanda.IdDemanda,
                x.Demanda.Titulo,
                x.Demanda.Descricao,
                x.IdRegistroServico,
                x.Mensagem,
                x.Prazo,
                x.ValorEstimado,
                Prestador = new
                {
                    x.Prestador.Id,
                    x.Prestador.Nome
                },
                Tags = x.Demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            });

            return Ok(propostas);
        }

        [HttpGet("demandas-abertas")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> GetDemandasDisponiveis()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var demandas_abertas = _dbContext.Set<Demanda>().Where(x => x.Estado == Domain.Enum.EstadoDemanda.DISPONIVEL && x.Tags.Any(y => user.Tags.Contains(y)) && !x.RegistroServicos.Any(y => y.IdPrestador == user.Id)).Select(x => new
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

        [HttpGet("propostas-enviadas")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> GetPropostasEnviadas()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var propostasEnviadas = _dbContext.Set<RegistroServico>().Where(x => x.Estado == Domain.Enum.EstadoServico.SELECIONADO && x.IdPrestador == user.Id);

            return Ok(propostasEnviadas.Select(x => new
            {
                x.Demanda.IdDemanda,
                x.Demanda.Titulo,
                x.Demanda.Descricao,
                x.ValorEstimado,
                x.Prazo,
                x.IdRegistroServico,
                Tags = x.Demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                }),
                Demandante = new
                {
                    x.Demandante.Nome,
                    x.Demandante.Email
                }
            }));
        }

        [HttpGet("propostas-topadas")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> GetPropostasTopadas()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var propostasTopadas = _dbContext.Set<RegistroServico>().Where(x => x.Estado == Domain.Enum.EstadoServico.TOPADO && x.IdPrestador == user.Id);

            return Ok(propostasTopadas.Select(x => new
            {
                x.Demanda.IdDemanda,
                x.Demanda.Titulo,
                x.Demanda.Descricao,
                x.ValorEstimado,
                x.Prazo,
                x.IdRegistroServico,
                Tags = x.Demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                }),
                Demandante = new
                {
                    x.Demandante.Nome,
                    x.Demandante.Email
                }
            }));
        }

        [HttpGet("propostas-avaliacao")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> GetPropostasAvaliacao()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var avaliarDemandas = _dbContext.Set<RegistroServico>().Where(x => x.Estado == Domain.Enum.EstadoServico.CONCLUIDO && x.IdPrestador == user.Id && !x.Avaliacoes.Any(y => y.IdUsuario == user.Id));

            return Ok(avaliarDemandas.Select(x => new
            {
                x.Demanda.IdDemanda,
                x.Demanda.Titulo,
                x.Demanda.Descricao,
                x.ValorEstimado,
                x.Prazo,
                x.IdRegistroServico,
                Tags = x.Demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                }),
                Demandante = new
                {
                    x.Demandante.Nome,
                    x.Demandante.Email
                }
            }));
        }
        #endregion
    }
}
