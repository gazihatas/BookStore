using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        //hangi id li kitabın detayını istiyorsak tanımlayacağız ki dışarıdan set edilebilsin
        public int BookId { get; set; }

        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı!");
            }

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}