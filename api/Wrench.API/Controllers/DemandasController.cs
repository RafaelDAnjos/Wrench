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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<ActionResult> GetDemandasUsuario()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            _dbContext.Entry(user).Collection(x => x.Tags).Load();

            var demandas_usuario = _dbContext.Set<Demanda>().Where(x => user.Id == x.IdDemandante || user.Id == x.IdPrestador).Select(x => new
            {
                x.IdDemanda,
                Demandante = new
                {
                    x.IdDemandante,
                    x.Demandante.Nome
                },
                Prestador = new
                {
                    x.IdPrestador,
                    x.Prestador.Nome
                },
                x.Titulo,
                x.Descricao,
                Tags = x.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            }).ToList();

            return Ok(demandas_usuario);
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
                x.IdDemandante,
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
                demanda.IdDemandante,
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
                demanda.IdDemandante,
                demanda.Titulo,
                demanda.Descricao,
                Tags = demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            });
        }

        [HttpPost("topar-demanda")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> ToparDemandaAsync(ToparDemandaViewModel value)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var demanda = _dbContext.Set<Demanda>().Find(value.IdDemanda);

            if (demanda == null)
                return NotFound("Demanda não encontrada.");

            demanda.ToparDemanda(user.Id);

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                demanda.IdDemanda,
                demanda.IdDemandante,
                demanda.Titulo,
                demanda.Descricao,
                Tags = demanda.Tags.Select(y => new
                {
                    y.IdTag,
                    y.Nome
                })
            });
        }
    }
}
