using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.EntityFrameworkCore
{
    public static class ETicaretDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ETicaretDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ETicaretDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
