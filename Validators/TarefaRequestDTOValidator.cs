using FluentValidation;
using TarefasApi.DTOs;

namespace TarefasApi.Validators
{
    public class TarefaRequestDTOValidator : AbstractValidator<TarefaRequestDTO>
    {
        public TarefaRequestDTOValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(100).WithMessage("A descrição não pode exceder 100 caracteres.")
                .Must(ValidarRegraDeConclusao).WithMessage("Se a tarefa está concluída, ela deve ter uma descrição válida contendo mais de 5 caracteres.");
        }

        private bool ValidarRegraDeConclusao(TarefaRequestDTO dto, string? descricao)
        {
            // Exemplo de regra complexa que o DataAnnotations não suportava
            if (dto.Concluida && (string.IsNullOrWhiteSpace(descricao) || descricao.Length <= 5))
            {
                return false;
            }
            return true;
        }
    }
}
