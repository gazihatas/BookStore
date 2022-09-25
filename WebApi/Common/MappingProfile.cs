using AutoMapper;
using WebApi.Application.AuthorOperations.Command.CreateAuthor;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Book
            CreateMap<CreateBookModel, Book>();
            //destination içersinde ki yani BooksViewModel içerisindeki Genre string deðerim   src daki GenreId nin Enumdaki karþýlýðý
            //CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name));

            //Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            //Author
            
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        }
    }
}