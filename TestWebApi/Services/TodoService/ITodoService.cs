namespace TestWebApi.Services.TodoService;

/// <summary>
/// todo 서비스
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Todo 저장 -> update/insert
    /// </summary>
    /// <param name="todos"></param>
    /// <returns></returns>
    public Task<int> SaveTodosAsync(List<Todo> todos);

    /// <summary>
    /// todo 삭제
    /// </summary>
    /// <param name="todoNo"></param>
    /// <returns></returns>
    public Task DeleteTodosAsync(int todoNo);

    /// <summary>
    /// todo 리스트 조회
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="title"></param>
    /// <param name="contents"></param>
    /// <returns></returns>
    public Task<IEnumerable<Todo>> FindTodosAsync(string? userId, string? title, string? contents);

    /// <summary>
    /// todo 번호로 조회
    /// </summary>
    /// <param name="todoNo"></param>
    /// <returns></returns>
    public Task<Todo?> FindTodoAsync(int todoNo);
}
