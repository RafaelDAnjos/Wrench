using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Wrench.API.ViewModels.Tags;
using Wrench.Data.Context;
using Wrench.Domain.Entities;

namespace Wrench.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly WrenchDbContext _dbContext;
        private readonly DbSet<Tag> _Tags;

        public TagsController(WrenchDbContext dbContext)
        {
            _Tags = dbContext.Set<Tag>();
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var result = await _Tags.Where(x => x.Ativo).ToListAsync();

            return Ok(result.Select(x => new { x.IdTag, x.Nome }));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await _Tags.FindAsync(id);

            return Ok(new { result.IdTag, result.Nome });
        }

        [HttpPost]
        public ActionResult Post(CadastrarTagViewModel value)
        {
            var tag = _Tags.FirstOrDefault(x => x.Nome == value.Nome);

            if (tag != null)
                return BadRequest(new { Errors = new[] { "Não pode haver tag com nome duplicado." } });

            tag = new Tag(value.Nome);

            _Tags.Add(tag);

            _dbContext.SaveChanges();

            return Ok(new { tag.IdTag, tag.Nome });
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, AtualizarTagViewModel value)
        {
            var tag = _Tags.Find(id);

            if (tag == null)
                return NotFound(id);

            var nomeDuplicado = _Tags.Any(x => x.IdTag != id && x.Nome == value.Nome);

            if (nomeDuplicado) return BadRequest(new { Errors = new[] { "Esse nome já está sendo usado por outra tag." } });

            tag.AlterarInformacao(value.Nome);

            _Tags.Update(tag);
            _dbContext.SaveChanges();

            return Ok(new { tag.IdTag, tag.Nome });
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var tag = _Tags.Find(id);

            if (tag == null)
                return NotFound(id);

            tag.Inativar();

            _Tags.Update(tag);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
