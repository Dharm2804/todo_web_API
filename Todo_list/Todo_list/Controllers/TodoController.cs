using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private static List<TodoItem> todos = new List<TodoItem>();

    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> Get() => Ok(todos);

    [HttpGet("{id}")]
    public ActionResult<TodoItem> Get(int id)
    {
        var todo = todos.Find(t => t.Id == id);
        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public ActionResult Post(TodoItem todo)
    {
        todo.Id = todos.Count + 1;
        todos.Add(todo);
        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, TodoItem todo)
    {
        var existingTodo = todos.Find(t => t.Id == id);
        if (existingTodo == null) return NotFound();

        existingTodo.Title = todo.Title;
        existingTodo.IsComplete = todo.IsComplete;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var todo = todos.Find(t => t.Id == id);
        if (todo == null) return NotFound();

        todos.Remove(todo);
        return NoContent();
    }
}
    