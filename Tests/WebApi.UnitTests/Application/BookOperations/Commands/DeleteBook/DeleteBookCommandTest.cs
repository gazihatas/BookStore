using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1000;

            //act (çalıştırma) - assert (Doğrulama)
            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }


        //Tüm koşulların Çalıştığı Yol - Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange (hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            //act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}
