namespace ToDoApp.API.Models;

public sealed class Todo
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDateTime { get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}    
