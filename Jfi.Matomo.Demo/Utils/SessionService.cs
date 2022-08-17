namespace Jfi.Matomo.Demo.Utils;

using Models;

using Newtonsoft.Json;

public class SessionService : ISessionService
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string SessionKey = "session-form";


    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SessionModel?> GetSessionModel()
    {
        SessionModel current = new();
        await _httpContextAccessor.HttpContext!.Session.LoadAsync();
        string? existingForm = _httpContextAccessor.HttpContext!.Session.GetString(SessionKey);
        if (!string.IsNullOrEmpty(existingForm))
            current = JsonConvert.DeserializeObject<SessionModel>(existingForm)!;
        return current;
    }
    public void SetSessionModel(SessionModel formModel)
    {
        _httpContextAccessor.HttpContext!.Session.SetString(SessionKey, JsonConvert.SerializeObject(formModel));
    }
}

public interface ISessionService
{
    Task<SessionModel?> GetSessionModel();
    void SetSessionModel(SessionModel sessionModel);
}