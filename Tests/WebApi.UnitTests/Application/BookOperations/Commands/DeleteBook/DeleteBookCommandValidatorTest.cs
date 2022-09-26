using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.DeleteBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            //arrange (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            //act (Çalıştırma)
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            //act (Çalıştırma)
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().Be(0);
        }
    }
}
