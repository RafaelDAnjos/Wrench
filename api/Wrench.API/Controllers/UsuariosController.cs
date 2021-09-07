using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wrench.API.ViewModels.Usuarios;
using Wrench.Data.Context;
using Wrench.Domain.Entities;
using Wrench.Domain.Entities.Identity;

namespace Wrench.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly WrenchDbContext _dbContext;

        public UsuariosController(UserManager<AppUser> userManager, WrenchDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetAsync()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (user == null)
                return NotFound(new { Errors = new[] { "Usuário não encontrado." } });

            await _dbContext.Entry(user).Collection(x => x.Tags).LoadAsync();

            return Ok(new
            {
                user.Id,
                user.Nome,
                user.Email,
                user.Tipo,
                Tags = user.Tags.Select(x => new { x.IdTag, x.Nome })
            });
        }

        [HttpPost("{userId:guid}/controle-tags")]
        [Authorize(Roles = "Prestador")]
        public async Task<ActionResult> PostAsync(Guid userId, ControleTagUsuarioViewModel value)
        {
            if (userId != Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value))
                return Unauthorized(new { Errors = new[] { "Usuário não possui permissão." } });

            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _dbContext.Entry(user).Collection(x => x.Tags).LoadAsync();

            var tags = await _dbContext.Set<Tag>().Where(x => value.Tags.Contains(x.Nome) && x.Ativo).ToListAsync();

            try
            {
                foreach (var tag in user.Tags.Where(x => !tags.Contains(x)))
                {
                    user.RemoverTag(tag);
                }

                foreach (var tag in tags.Except(user.Tags))
                {
                    user.AdicionarTag(tag);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
