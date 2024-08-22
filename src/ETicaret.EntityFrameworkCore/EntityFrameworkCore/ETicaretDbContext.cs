using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ETicaret.Authorization.Roles;
using ETicaret.Authorization.Users;
using ETicaret.MultiTenancy;
using ETicaret.Entities;
using ETicaret.Storage;

namespace ETicaret.EntityFrameworkCore
{
    public class ETicaretDbContext : AbpZeroDbContext<Tenant, Role, User, ETicaretDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<BinaryObject> BinaryObjects { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ETicaretDbContext(DbContextOptions<ETicaretDbContext> options)
            : base(options)
        {
        }

    }
}
