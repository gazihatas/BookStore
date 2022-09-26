using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommand : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommand(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }



        //var olan kitabın ismi verildiğinde geriye InvalidOperationException dönsün istiyorum.
        //Bana zaten var olan kitabın title ı verikldiğinde - InvalidOperationException - Hatasını Geriye Döndürmeli
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var book = new Book() { Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new System.DateTime(1990, 01,10), GenreId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();

            WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
        }

        [Fact]
        public void WhenValidInpudAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            WebApi.BookOperations.CreateBook.CreateBookCommand command = new WebApi.BookOperations.CreateBook.CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel() {Title ="Hobbit", PageCount =100, PublishDate=DateTime.Now.Date.AddYears(-10)};
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }

    }
}
