namespace Jfi.Matomo.Demo.Pages;

using Jfi.Matomo.Demo.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class FormCompleteModel : PageModel
{
    private readonly ISessionService _sessionService;

    public FormCompleteModel(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<IActionResult> OnGet()
    {
        var s = await _sessionService.GetSessionModel();

        if (s is null)
            return RedirectToPage("FormStart");

        return Page();
    }
}
