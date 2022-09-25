using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator :AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            //Kuralı koşula bağlamak
            //Boş gelmezse minimum uzunluk 4 olsun 
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
        }
    }
}
