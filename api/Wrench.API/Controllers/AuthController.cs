using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wrench.API.ViewModels;
using Wrench.Domain.Entities.Identity;

namespace Wrench.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(ILogger<AuthController> logger,
                              SignInManager<AppUser> signInManager,
                              UserManager<AppUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioViewModel registrarUsuarioViewModel)
        {
            var user = await _userManager.FindByEmailAsync(registrarUsuarioViewModel.Email);

            if (user != null)
                return BadRequest(new { Sucesso = false, Errors = new[] { "Email já está cadastrado" } });

            user = new AppUser
            {
                Nome = registrarUsuarioViewModel.Nome,
                UserName = registrarUsuarioViewModel.Email,
                Email = registrarUsuarioViewModel.Email,
                Tipo = registrarUsuarioViewModel.TipoUsuario,
                Identificacao = registrarUsuarioViewModel.Identificacao,
            };

            var result = await _userManager.CreateAsync(user, registrarUsuarioViewModel.Senha);

            if (!result.Succeeded)
                return BadRequest(new { Sucesso = false, Errors = new[] { "Houve um erro ao cadastrar o usuário" } });

            return Ok(new { Sucesso = true, user.Nome, user.Email, Token = GenerateToken(user) });
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUsuario(LoginUsuarioViewModel loginUsuarioViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginUsuarioViewModel.Email);

            if (user != null)
                return NotFound(new { Sucesso = false, Errors = new[] { "Usuário inexistente" } });

            var result = await _signInManager.PasswordSignInAsync(user, loginUsuarioViewModel.Senha, false, true);

            if (result.IsLockedOut)
                return BadRequest(new { Sucesso = false, Errors = new[] { "Esse usuário alcançou o limite máximo de tentativas" } });

            if (result.IsNotAllowed)
                return BadRequest(new { Sucesso = false, Errors = new[] { "Esse usuário não pode efetuar login atualmente" } });

            if (!result.Succeeded)
                return BadRequest(new { Sucesso = false, Errors = new[] { "Senha incorreta" } });

            return Ok(new { Sucesso = true, user.Nome, user.Email, Token = GenerateToken(user) }); ;
        }

        private static string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DISCIPLINALES20212X");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    user.Tipo == TipoUsuario.DEMANDANTE ? new Claim(ClaimTypes.Role, "Demandante") : new Claim(ClaimTypes.Role, "Prestador")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}