using MermaidSharp.EntityFrameworkCore.Tests.Mocks.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MermaidSharp.EntityFrameworkCore.Tests.Mock
{
    internal class DatabaseIdentityContextMock : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DatabaseIdentityContextMock(DbContextOptions<DatabaseIdentityContextMock> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
