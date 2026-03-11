namespace TarefasApi.DTOs
{
    public class TarefaResponseDTO
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public bool Concluida { get; set; }
    }
}
