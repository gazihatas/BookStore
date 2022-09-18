using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
            
        }
        //Books entity nin tüm değerlerine erişebiliriz.
        public DbSet<Book> Books { get; set; }
    }
}