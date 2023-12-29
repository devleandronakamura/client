using Client.Application.DTOs.InputModels;
using Client.Domain.Enums;
using Client.Util;
using FluentValidation;

namespace Client.Application.Validators
{
    public class AddCustomerInputModelValidator : AbstractValidator<AddCustomerInputModel>
    {
        public AddCustomerInputModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("O nome é não pode ser em branco.");

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("O nome não pode ser nulo.");

            RuleFor(x => x.FirstName)
                .MaximumLength(50)
                .WithMessage("O tamanho máximo do nome é de 50 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("O sobrenome é não pode ser em branco.");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("O sobrenome não pode ser nulo.");

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .WithMessage("O tamanho máximo do sobrenome é de 50 caracteres.");

            RuleFor(x => x.DocumentNumber)
                .NotNull().WithMessage("CPF não pode ser nulo.")
                .When(x => x.DocumentType == EDocumentType.CPF);

            RuleFor(x => x.DocumentNumber)
                .NotEmpty().WithMessage("CPF não pode ser em branco.")
                .When(x => x.DocumentType == EDocumentType.CPF);

            RuleFor(x => x.DocumentNumber)
                .Must(BeValidCpf).WithMessage("CPF inválido.")
                .When(x => x.DocumentType == EDocumentType.CPF);
        }

        private bool BeValidCpf(string cpf) 
            => Util.Validators.IsCpfValid(cpf);
    }
}
