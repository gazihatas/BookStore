using FluentValidation;
using System;

namespace WebApi.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(author => author.AuthorId).GreaterThan(0);
            RuleFor(author => author.Model.GenreId).GreaterThan(0);
            RuleFor(author => author.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
            RuleFor(author => author.Model.Surname).MinimumLength(4).When(x => x.Model.Surname != string.Empty);
            RuleFor(author => author.Model.DateOfBirth).LessThanOrEqualTo(DateTime.Now.AddYears(-3));
        }
    }
}
