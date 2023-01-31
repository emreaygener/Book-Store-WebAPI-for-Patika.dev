using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; } 
        public CommonTestFixture()
        {
            var options =  new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB");
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();
        }
        
    }
}