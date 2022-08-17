namespace Jfi.Matomo.Demo.Pages;

using Jfi.Matomo.Demo.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class FormStartModel : PageModel
{
    private readonly ISessionService _sessionService;

    public FormStartModel(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [BindProperty]
    public string FullName { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var sessionModel = (await _sessionService.GetSessionModel()) ?? new();

        sessionModel.FullName = FullName;
        sessionModel.Email = Email;

        _sessionService.SetSessionModel(sessionModel);

        return RedirectToPage("FormMiddle");
    }
}
