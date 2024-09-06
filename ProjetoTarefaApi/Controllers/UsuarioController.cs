using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Models; // Ajuste o namespace conforme necessário
using ProjetoTarefaApi.Services; // Importar o namespace onde o TokenService está localizado
using System.ComponentModel.DataAnnotations; // Importar o namespace correto
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

            // Adiciona o usuário ao banco
               // Adiciona o usuário ao banco de dados
               _context.Usuarios.Add(usuario);
               await _context.SaveChangesAsync();

               // Retorna o usuário criado com um código de status 201 (Criado)
               // Retorna o ID gerado automaticamente
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

               // Gerar o token JWT
               var token = _tokenService.GenerateToken(usuario.Email);

               // Retorna o token JWT
               return Ok(new { Token = token });
           }
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
