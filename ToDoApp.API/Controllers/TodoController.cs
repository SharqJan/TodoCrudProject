using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.API.Data;
using ToDoApp.API.Models;

namespace ToDoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext _todoDbContext;

    public TodoController(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await _todoDbContext.Todos.Where(x => x.IsDelete == false)
            .OrderByDescending(x => x.CreatedDateTime)
            .ToListAsync();
            
        return Ok(todos);
    }
    [HttpGet]
    [Route("get-deleted-todos")]
    public async Task<IActionResult> GetDeletedTodos()
    {
        var todo = await _todoDbContext.Todos
            .Where(x => x.IsDelete == true)
            .OrderByDescending(x => x.CreatedDateTime)
            .ToListAsync();
       
        return Ok(todo);
    }
    [HttpPost]
    public async Task<IActionResult> AddTodo(Todo todo)
    {
        todo.Id = Guid.NewGuid();
        _todoDbContext.Todos.Add(todo);
        await _todoDbContext.SaveChangesAsync();
        return Ok(todo);
    }
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, Todo todoUpdateRequest)
    {

        var todo = await _todoDbContext.Todos.FindAsync(id);
        if(todo == null)
        {

            return NotFound();
        }
        todo.IsCompleted = todoUpdateRequest.IsCompleted;
        todo.CompletedDateTime = DateTime.Now;
        await _todoDbContext.SaveChangesAsync();
        
        return Ok(todo);
    }
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
    {
        var todo = await _todoDbContext.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }
        todo.IsDelete = true;
        todo.DeletedDateTime = DateTime.Now;
        await _todoDbContext.SaveChangesAsync();
        return Ok(todo);
    }
    [HttpPut]
    [Route("undo-deleted-todo/{id:Guid}")]
    public async Task<IActionResult> UndoDeleteTodo([FromRoute] Guid id, Todo undoDeleteTodoRequest)
    {
        var todo = await _todoDbContext.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }
      
        todo.DeletedDateTime = null;
        todo.IsDelete = false;
        await _todoDbContext.SaveChangesAsync();

        return Ok(todo);
    }
}

