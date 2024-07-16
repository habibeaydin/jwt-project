using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.ToDoItems)
                    .WithOne(i => i.User)
                    .HasForeignKey(i => i.UserId);
            });

            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.HasOne(x => x.User)
                      .WithMany(u => u.ToDoItems)
                      .HasForeignKey(x => x.UserId);
            });
        }
    }
}
