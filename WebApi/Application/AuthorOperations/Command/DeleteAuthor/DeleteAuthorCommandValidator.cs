using FluentValidation;

namespace WebApi.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}
