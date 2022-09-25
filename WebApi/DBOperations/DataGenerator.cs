using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Test 1",
                        Surname = "Demo 1",
                        DateOfBirth = new DateTime(1999, 08,01),
                        GenreId = 1
                    },
                    new Author
                    {
                        Name = "Test 2",
                        Surname = "Demo 2",
                        DateOfBirth = new DateTime(1989, 08, 02),
                        GenreId = 2
                    },
                    new Author
                    {
                        Name = "Test 3",
                        Surname = "Demo 3",
                        DateOfBirth = new DateTime(1970, 08, 03),
                        GenreId= 3
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }

                 );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lan Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, 
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}