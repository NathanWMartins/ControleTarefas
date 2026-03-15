namespace TarefasApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public bool Concluida { get; set; } = false;
        public bool Excluida { get; set; } = false;
        public DateTime? DataExclusao { get; set; }

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}