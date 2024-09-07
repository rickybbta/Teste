using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTarefaApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime DataRealizacao { get; set; }

        [Required]
        public string Status { get; set; } = "Pendente";

        // Relacionamento com o usuário
        public int UsuarioId { get; set; }
    }
}
