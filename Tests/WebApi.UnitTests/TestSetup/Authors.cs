using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                   new Author { Name = "Test 1",Surname = "Demo 1", DateOfBirth = new DateTime(1999, 08, 01), GenreId = 1 },
                   new Author { Name = "Test 2",Surname = "Demo 2", DateOfBirth = new DateTime(1989, 08, 02), GenreId = 2 },
                   new Author { Name = "Test 3", Surname = "Demo 3", DateOfBirth = new DateTime(1970, 08, 03), GenreId = 3 }
               );
        }
    }
}
