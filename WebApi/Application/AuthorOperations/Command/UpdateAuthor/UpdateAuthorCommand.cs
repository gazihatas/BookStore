using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using System;
using WebApi.DBOperations;
using System.Linq;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApi.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
       
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            
            var genre = _context.Authors.Include(x => x.Genre).SingleOrDefault(x => x.Genre.Id == Model.GenreId);
            if (genre is null)
                throw new InvalidOperationException("Güncellemek istenilen kitap türü mevcut değildir!");

           
            /*
            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            */
            _mapper.Map<UpdateAuthorModel, Author>(Model, author);
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenreId { get; set; }
    }
}
