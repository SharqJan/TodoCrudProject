using Microsoft.EntityFrameworkCore;
using ToDoApp.API.Models;
namespace ToDoApp.API.Data;

public sealed class TodoDbContext : DbContext
{
    public TodoDbContext (DbContextOptions options) : base(options) { }
    public DbSet<Todo> Todos { get; set; } 
}
