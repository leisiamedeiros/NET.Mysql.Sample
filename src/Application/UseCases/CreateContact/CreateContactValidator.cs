using FluentValidation;

namespace NET.Mysql.Sample.Application.UseCases.CreateContact
{
    public class CreateContactValidator : AbstractValidator<CreateContactInput>
    {
        public CreateContactValidator()
        {
            RuleFor(input => input.Name).NotEmpty();

            RuleFor(input => input.Email).NotEmpty().EmailAddress();
        }
    }
}
