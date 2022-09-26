using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId { get; set; }


        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException(" Yazar Mevcut Değil");

            var book = _context.Books.Include(x => x.Author).SingleOrDefault(x => x.Author.Id == AuthorId);
            if (book is not null)
                throw new InvalidOperationException(" Bu yazara ait en az bir kitap var, yazar silinemez");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
