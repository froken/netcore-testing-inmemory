using Microsoft.EntityFrameworkCore;
using Todo.Database.Models;

namespace Todo.Database.Contexts
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoTask> Tasks { get; set; }

        public TodoContext(DbContextOptions options) : base(options)
        {
        }
    }
}
