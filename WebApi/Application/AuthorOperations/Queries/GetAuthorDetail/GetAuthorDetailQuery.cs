using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        //hangi id li kitabın detayını istiyorsak tanımlayacağız ki dışarıdan set edilebilsin
        public int BookId { get; set; }

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.Include(x => x.Genre).Where(author => author.Id == AuthorId).SingleOrDefault();
            if (author is null)
            {
                throw new InvalidOperationException(" Aranılan Yazar Bulunamadı!");
            }

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Genre { get; set; }
    }
}
