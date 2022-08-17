namespace Jfi.Matomo.Demo.Pages;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CookiesModel : PageModel
{
    public const string SessionCookieName = ".Jfi.MatomoDemo.Session";
    public const string ConsentCookieName = ".Jfi.MatomoDemo.CookieConsent";

    [BindProperty]
    public string? ConsentCookieValue { get; set; }

    public IActionResult OnGet()
    {
        // TODO: Load in cookie preferences, should we add analytics
        Request.Cookies.TryGetValue(ConsentCookieName, out string? consentValue);
        ConsentCookieValue = consentValue;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!string.IsNullOrEmpty(ConsentCookieValue))
            Response.Cookies.Append(ConsentCookieName, ConsentCookieValue);

        return Page();
    }

    public IActionResult OnPostAccept()
    {
        // todo - only grant consent on approval if analytics involved
        Response.Cookies.Append(ConsentCookieName, "accept");
        var referrer = Request.Headers.Referer.ToString();
        return Redirect(referrer);
    }

    public IActionResult OnPostReject()
    {
        // todo - only grant consent on approval if analytics involved
        Response.Cookies.Append(ConsentCookieName, "reject");
        var referrer = Request.Headers.Referer.ToString();
        return Redirect(referrer);
    }
}
