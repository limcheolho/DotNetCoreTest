using System.Transactions;

namespace TestWebApi.Services.TodoService;

/// <summary>
/// todo 서비스
/// </summary>
public class TodoService : ITodoService
{
    private readonly TestDbContext _dbContext;
    private readonly SystemInfoExtensions _systemInfoExtensions;

    /// <summary>
    /// 생성자
    /// /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="systemInfoExtensions"></param>
    public TodoService(TestDbContext dbContext, SystemInfoExtensions systemInfoExtensions)
    {
        _dbContext = dbContext;
        _systemInfoExtensions = systemInfoExtensions;
    }

    private async Task<int> InsertTodoAsync(Todo todo)
    {
        _systemInfoExtensions.SetModelLogInfo(todo);
        await _dbContext.AddAsync(todo);

        var successCount = await _dbContext.SaveChangesAsync();
        return successCount;
    }

    private async Task<int> UpdateTodoAsync(Todo todo)
    {
        var data =
            await _dbContext.Todos.SingleOrDefaultAsync(g => g.todoNo == todo.todoNo)
            ?? throw new InvalidOperationException($"업데이트할 데이터가 없습니다. {todo.todoNo}");
        data.title = todo.title;
        data.contents = todo.contents;
        data.isValid = todo.isValid;
        _systemInfoExtensions.SetModelLogInfo(data);

        var successCount = await _dbContext.SaveChangesAsync();
        return successCount;
    }

    public async Task<int> SaveTodosAsync(List<Todo> todos)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var insertCount = 0;
        var updateCount = 0;
        foreach (var todo in todos)
        {
            if (todo.todoNo == 0)
                insertCount += await InsertTodoAsync(todo);
            else
                updateCount += await UpdateTodoAsync(todo);
        }

        await UpdateUserTodoCountAsync(todos.FirstOrDefault()!.userId, insertCount, isPlus: true);
        return insertCount + updateCount;
    }


    public async Task DeleteTodosAsync(int todoNo)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var data = await _dbContext.Todos.SingleOrDefaultAsync(g => g.todoNo == todoNo);
        if (data == null)
            return;

        await UpdateUserTodoCountAsync(data.userId, 1, isPlus: false);

        _dbContext.RemoveRange(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Todo>> FindTodosAsync(
        string? userId,
        string? title,
        string? contents
    )
    {
        var data = await _dbContext
            .Todos.Where(g =>
                (
                    string.IsNullOrEmpty(userId)
                    || !string.IsNullOrEmpty(userId) && g.userId == userId
                )
                && (
                    string.IsNullOrEmpty(title)
                    || !string.IsNullOrEmpty(title) && g.title.Contains(title)
                )
                && (
                    string.IsNullOrEmpty(contents)
                    || !string.IsNullOrEmpty(contents) && g.title.Contains(contents)
                )
            )
            .AsNoTracking()
            .Include(g => g.user)
            .ThenInclude(g => g.refreshTokens)
            .ToListAsync();
        return data;
    }

    public async Task<Todo?> FindTodoAsync(int todoNo)
    {
        var data = await _dbContext.Todos.SingleOrDefaultAsync(g => g.todoNo == todoNo);
        return data;
    }

    private async Task UpdateUserTodoCountAsync(string userId, int updateCount, bool isPlus)
    {
        var data = await _dbContext.Users.FirstOrDefaultAsync(g => g.userId == userId);
        if (isPlus)
            data!.totalTodos += updateCount;
        else
            data!.totalTodos -= updateCount;

        await _dbContext.SaveChangesAsync();
    }
}
