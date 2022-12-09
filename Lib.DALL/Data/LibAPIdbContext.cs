using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Lib.BLL.Models;

public class LibAPIdbContext : IdentityDbContext<IdentityUser>
    {
        public LibAPIdbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Author { get; set; }

        public DbSet<Book> Book { get; set; }

}
