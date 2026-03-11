using System.ComponentModel.DataAnnotations;

namespace TarefasApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória.")]
        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string? Descricao { get; set; }
        public bool Concluida { get; set; } = false;
    }
}