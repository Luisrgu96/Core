using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public class MyDbContext : DbContext
    {
        // DbSet<SomeModel> {get;set;}

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}
