using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Models {

    // this class is inheriting from DbContext
    public class TodoContext : DbContext {

        public TodoContext(DbContextOptions<TodoContext> options) : base(options) {
        
        }

        // Just like Java this is <DataType>
        public DbSet<TodoItem> TodoItems { get; set;}
        public DbSet<Student> Student { get; set; }

    }
}
