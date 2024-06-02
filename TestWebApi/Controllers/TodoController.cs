using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Services.TodoService;

namespace TestWebApi.Controllers;

[Route("todo")]
[ApiController]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [Route("list")]
    [HttpGet]
    public async Task<IActionResult> GetTodos(string? userId, string? title, string? contents)
    {
        var data = await _todoService.FindTodosAsync(userId, title, contents);
        return Ok(data);
    }

    [Route("one/{todoNo}")]
    [HttpGet]
    public async Task<IActionResult> GetTodo(int todoNo)
    {
        var data = await _todoService.FindTodoAsync(todoNo);
        return Ok(data);
    }

    [Route("save")]
    [HttpPost]
    public async Task<IActionResult> SaveTodos(List<Todo> todos)
    {
        var count = await _todoService.SaveTodosAsync(todos);

        return Ok(count);
    }

    [Route("delete/{todoNo}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTodo(int todoNo)
    {
        await _todoService.DeleteTodosAsync(todoNo);
        return Ok();
    }
}
