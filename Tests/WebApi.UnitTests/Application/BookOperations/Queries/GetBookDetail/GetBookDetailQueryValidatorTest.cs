using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.GetBookDetail;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(null, null);
            bookDetailQuery.BookId = bookId;
            // act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(bookDetailQuery);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            // arrange
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(null, null);
            bookDetailQuery.BookId = 1;
            // act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(bookDetailQuery);
            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
