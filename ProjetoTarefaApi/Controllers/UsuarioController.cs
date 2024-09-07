using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Models; // Ajuste o namespace conforme necessário
using ProjetoTarefaApi.Services; // Importar o namespace onde o TokenService está localizado
using ProjetoTarefaApi.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProjetoTarefaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UsuarioController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // POST: api/usuario/cadastro
        [HttpPost("cadastro")]
        public async Task<ActionResult<Usuario>> Cadastro([FromBody] Usuario usuario)
        {
            if (usuario == null || !ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            // Verifica se o email já está cadastrado
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                return Conflict("Email já cadastrado.");
            }

            // Adiciona o usuário ao banco
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Cadastro), new { id = usuario.Id }, usuario);
        }

        // POST: api/usuario/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            // Verifica se o usuário existe e a senha está correta
            var usuario = await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Email == request.Email && u.Senha == request.Senha);

            if (usuario == null)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            // Gerar o token JWT com o ID do usuário
            var token = _tokenService.GenerateToken(usuario.Email, usuario.Id);

            // Retorna apenas o token JWT
            return Ok(new { Token = token });
        }

        // GET: api/usuario/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            // Obtém o ID do usuário autenticado
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null || int.Parse(userId) != id)
            {
                return Unauthorized("Você não tem permissão para acessar este recurso.");
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }

        // PUT: api/usuario/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuarioAtualizado)
        {
            if (usuarioAtualizado == null || !ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            // Obtém o ID do usuário autenticado
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null || int.Parse(userId) != id)
            {
                return Unauthorized("Você não tem permissão para alterar este recurso.");
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Atualizar os campos permitidos
            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Telefone = usuarioAtualizado.Telefone;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erro ao atualizar o usuário.");
            }

            return NoContent();
        }

        // DELETE: api/usuario/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            // Obtém o ID do usuário autenticado
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null || int.Parse(userId) != id)
            {
                return Unauthorized("Você não tem permissão para excluir este recurso.");
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // Modelo para a solicitação de login
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Inicializa para evitar warnings

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty; // Inicializa para evitar warnings
    }
}
