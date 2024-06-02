namespace TestWebApi.Services.TodoService;

public interface ITodoService
{
    

    public Task<int> SaveTodosAsync(List<Todo> todos);

    public Task DeleteTodosAsync(int todoNo);

    public Task<IEnumerable<Todo>> FindTodosAsync(string? userId, string? title, string? contents);

    public Task<Todo?> FindTodoAsync(int todoNo);
}