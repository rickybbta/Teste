using System.ComponentModel.DataAnnotations;

namespace ProjetoTarefaApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
