using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0, "Lor", 1)]
        [InlineData(0, "Lord", 0)]
        [InlineData(0, "", 0)]
        [InlineData(1, "", 1)]
        [InlineData(1, "", 0)]
        [InlineData(1, " ", 1)]
        [InlineData(1, "Lord of The Rings", 0)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            
            //assert 
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel()
            {
                Title = "Test Book",
                GenreId = 1,
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

    }
}
