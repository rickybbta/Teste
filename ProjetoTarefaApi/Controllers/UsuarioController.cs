using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Models;
using ProjetoTarefaApi.Services;
using System.ComponentModel.DataAnnotations;
using ProjetoTarefaApi.Data;

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

            // Adiciona o usuário ao banco de dados
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuario.Senha = null; // Não retornar a senha

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

            // Buscar usuário pelo email
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == request.Email);

            // Log de depuração para verificar o usuário encontrado e a senha armazenada
            if (usuario != null)
            {
                Console.WriteLine($"Usuário encontrado: Email = {usuario.Email}, Senha armazenada = {usuario.Senha}");
            }
            else
            {
                Console.WriteLine($"Nenhum usuário encontrado com o Email = {request.Email}");
            }

            // Verificar se o usuário existe e a senha é igual
            if (usuario == null || usuario.Senha != request.Senha)
            {
                // Log no console para depuração
                Console.WriteLine($"Falha de login: Email = {request.Email}, Senha fornecida = {request.Senha}");

                return Unauthorized("Email ou senha inválidos.");
            }

            // Gerar o token JWT
            var token = _tokenService.GenerateToken(usuario.Email);

            // Log no console
            Console.WriteLine($"Usuário logado: {usuario.Email}, Logado com sucesso!");

            // Retornar o token e o ID do usuário
            return Ok(new { Token = token, Id = usuario.Id });
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id || !ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Atualiza os campos do usuário existente
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Telefone = usuario.Telefone;
            
            // Atualiza a senha diretamente
            usuarioExistente.Senha = usuario.Senha;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Modelo para a solicitação de login
        public class LoginRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Senha { get; set; }
        }
    }
}
