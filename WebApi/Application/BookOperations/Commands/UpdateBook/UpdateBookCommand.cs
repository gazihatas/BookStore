using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _context;

        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
             var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null)
            {
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");
            }
            //verisi doldurulmuşssa updatedBook.GenreId kullan yoksa book.GenreId
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
             book.Title = Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();

             //PageCount ve PublishDate dışarıdan güncellenebilir bir bilgi değildir.
           // book.PageCount = Model.PageCount  != default ? updatedBook.PageCount : book.PageCount;
           // book.PublishDate = updatedBook.PublishDate  != default ? updatedBook.PublishDate : book.PublishDate;
           

            
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }

    }
}