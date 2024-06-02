namespace TestWebApi.Models;


public class ReponseModel<T>
{
    /// <summary>
    ///     생성자
    /// </summary>
    public ReponseModel() { }

    public ReponseModel(bool isSuccess, T message)
    {
        this.isSuccess = isSuccess;
        this.message = message;
    }

    public ReponseModel(bool isSuccess)
    {
        this.isSuccess = isSuccess;
    }

    public bool isSuccess { get; set; }
    public T message { get; set; }
}