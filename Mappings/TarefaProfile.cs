using AutoMapper;
using TarefasApi.Models;
using TarefasApi.DTOs;

namespace TarefasApi.Mappings
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<TarefaRequestDTO, Tarefa>();
            CreateMap<Tarefa, TarefaResponseDTO>();
        }
    }
}
