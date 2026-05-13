using MermaidSharp.EntityFramework.Tests.Mocks.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace MermaidSharp.EntityFramework.Tests.Mock
{
    public class DatabaseIdentityContextMock : DbContext
    {
        public DatabaseIdentityContextMock(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        internal virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Student configuration
            modelBuilder.Entity<User>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }
    }
}
