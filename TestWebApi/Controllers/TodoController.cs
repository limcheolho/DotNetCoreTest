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

    /// <summary>
    /// 투두 리스트
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="title"></param>
    /// <param name="contents"></param>
    /// <returns></returns>
    [Route("list")]
    [HttpGet]
    [ProducesResponseType(typeof(List<Todo>), 200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]

    public async Task<IActionResult> GetTodos(string? userId, string? title, string? contents)
    {
        var data = await _todoService.FindTodosAsync(userId, title, contents);
        return Ok(data);
    }

    /// <summary>
    /// 투두 번호로 투두 찾기
    /// </summary>
    /// <param name="todoNo"></param>
    /// <returns></returns>
    [Route("one/{todoNo}")]
    [HttpGet]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(Todo), 200)]

    public async Task<IActionResult> GetTodo(int todoNo)
    {
        var data = await _todoService.FindTodoAsync(todoNo);
        return Ok(data);
    }

    /// <summary>
    /// 투두 저장
    /// </summary>
    /// <param name="todos"></param>
    /// <returns></returns>
    [Route("save")]
    [HttpPost]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> SaveTodos(List<Todo> todos)
    {
        var count = await _todoService.SaveTodosAsync(todos);

        return Ok(count);
    }

    /// <summary>
    /// 투두 삭제
    /// </summary>
    /// <param name="todoNo"></param>
    /// <returns></returns>
    [Route("delete/{todoNo}")]
    [HttpDelete]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteTodo(int todoNo)
    {
        await _todoService.DeleteTodosAsync(todoNo);
        return Ok();
    }
}
