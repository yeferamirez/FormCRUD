using FluentValidation;
using Form.Domain.Entities;

namespace Form.Business.Utils
{
    public class UserValidations : AbstractValidator<User>
    {
        public UserValidations()
        {
            ValidateNames();
            ValidateLastNames();
            ValidateDate();
            ValidateSalary();
        }

        private void ValidateNames()
        {
            RuleFor(user => user.FirstName)
           .NotEmpty().WithMessage("El nombre es obligatorio.")
           .Length(1, 50).WithMessage("El nombre no puede exceder los {MaxLength} caracteres.")
           .Matches(@"^[^0-9]*$").WithMessage("El nombre no puede contener números.");

            RuleFor(user => user.SecondName)
            .Length(1, 50).WithMessage("El segundo nombre no puede exceder los {MaxLength} caracteres.")
            .Matches(@"^[^0-9]*$").WithMessage("El segundo nombre no puede contener números.");
        }

        private void ValidateLastNames()
        {
            RuleFor(user => user.FirstLastname)
            .NotEmpty().WithMessage("El primer apellido es obligatorio.")
            .Length(1, 50).WithMessage("El primer apellido no puede exceder los {MaxLength} caracteres.")
            .Matches(@"^[^0-9]*$").WithMessage("El primer apellido no puede contener números.");

            RuleFor(user => user.SecondLastname)
            .Length(1, 50).WithMessage("El segundo apellido no puede exceder los {MaxLength} caracteres.")
            .Matches(@"^[^0-9]*$").WithMessage("El segundo apellido no puede contener números.");
        }

        public void ValidateDate()
        {
            RuleFor(user => user.BirthDate)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatorio.")
            .Must(ValidationRequestUtil.IsValidDate).WithMessage("El formato de la fecha debe ser 'yyyy-MM-dd'.");
        }

        private void ValidateSalary()
        {
            RuleFor(user => user.Salary)
            .NotEmpty().WithMessage("El salario es obligatorio.")
            .When(user => user.Salary == 0).WithMessage("El salario debe ser mayor a cero.");
        }
    }
}
