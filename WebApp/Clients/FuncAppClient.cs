namespace WebApp.Clients;

public interface IFuncAppClient
{
    Task<string> GetGreetings();
}

public class FuncAppClient(HttpClient http) : IFuncAppClient
{
    private readonly HttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<string> GetGreetings()
    {
        var result = await this._http.GetStringAsync("api/helloworld");

        return result;
    }
}
