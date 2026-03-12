namespace TarefasApi.DTOs
{
    public class TarefaRequestDTO
    {
        public string? Descricao { get; set; }
        public bool Concluida { get; set; } = false;
    }
}
