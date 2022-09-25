using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower());
            if (author is not null)
                throw new InvalidOperationException(" Bu isim ve soyisim'e ait yazar yok!");

            var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == Model.GenreId);
            if (genre is null)
                throw new InvalidOperationException(" Kitap türü olmadığı için yazar ekleme işlemi başarısız!");

            author = _mapper.Map<Author>(Model);
            _dbContext.Add(author);
            _dbContext.SaveChanges();
        }

    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenreId { get; set; }
    }
}
