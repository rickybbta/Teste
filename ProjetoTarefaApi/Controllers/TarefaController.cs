using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Models;
using ProjetoTarefaApi.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTarefaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa([FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifica se o usuário associado existe
            var usuarioExistente = await _context.Usuarios.FindAsync(tarefa.UsuarioId);
            if (usuarioExistente == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarefaByUsuario), new { usuarioId = tarefa.UsuarioId, id = tarefa.Id }, tarefa);
        }

        // GET: api/tarefa/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefasByUsuario(int usuarioId)
        {
            var tarefas = await _context.Tarefas
                .Where(t => t.UsuarioId == usuarioId)
                .OrderBy(t => t.DataRealizacao)
                .ToListAsync();

            return Ok(tarefas);
        }

        // GET: api/tarefa/usuario/{usuarioId}/{id}
        [HttpGet("usuario/{usuarioId}/{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefaByUsuario(int usuarioId, int id)
        {
            var tarefa = await _context.Tarefas
                .Where(t => t.UsuarioId == usuarioId && t.Id == id)
                .FirstOrDefaultAsync();

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        // PUT: api/tarefa/{usuarioId}/{id}
        [HttpPut("usuario/{usuarioId}/{id}")]
        public async Task<IActionResult> PutTarefa(int usuarioId, int id, [FromBody] Tarefa tarefa)
        {
            if (id != tarefa.Id || usuarioId != tarefa.UsuarioId || !ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            var tarefaExistente = await _context.Tarefas
                .Where(t => t.Id == id && t.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (tarefaExistente == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            // Atualiza os campos da tarefa existente
            tarefaExistente.Nome = tarefa.Nome;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.DataRealizacao = tarefa.DataRealizacao;
            tarefaExistente.Status = tarefa.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tarefa/{usuarioId}/{id}
        [HttpDelete("usuario/{usuarioId}/{id}")]
        public async Task<IActionResult> DeleteTarefa(int usuarioId, int id)
        {
            var tarefa = await _context.Tarefas
                .Where(t => t.Id == id && t.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
