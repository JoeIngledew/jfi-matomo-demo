namespace Jfi.Matomo.Demo.Pages;

using Jfi.Matomo.Demo.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class FormMiddleModel : PageModel
{
    private readonly ISessionService _sessionService;

    public FormMiddleModel(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [BindProperty]
    public string Description { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var s = await _sessionService.GetSessionModel();

        if (s is null)
            return RedirectToPage("FormStart");

        s.Description = Description;

        _sessionService.SetSessionModel(s);

        return RedirectToPage("FormComplete");
    }
}
