using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var book = new Book { Title = "WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990, 02, 02), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            _context.Books.Remove(book);
            _context.SaveChanges();

            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = book.Id;

            // act - assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kayıt bulunamadı.");
        }

        [Fact]
        public void WhenGivenBookIdDoesExistInDb_Book_ShouldBeReturned()
        {
            // Arrange
            var book = new Book { Title = "WhenGivenBookIdDoesExistInDb_Book_ShouldBeRetuned", PageCount = 100, PublishDate = new DateTime(1990, 02, 02), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = book.Id;

            // Act
            var bookReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            bookReturned.Should().NotBeNull();
            bookReturned.Id.Should().Be(book.Id);
            bookReturned.Title.Should().Be(book.Title);
            bookReturned.PageCount.Should().Be(book.PageCount);
        }
    }
}
