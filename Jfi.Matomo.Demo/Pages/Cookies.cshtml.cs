namespace Jfi.Matomo.Demo.Pages;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CookiesModel : PageModel
{
    public string SessionCookieName => ".Jfi.MatomoDemo.Session";
    public string ConsentCookieName => ".Jfi.MatomoDemo.CookieConsent";

    public IActionResult OnGet()
    {
        // TODO: Load in cookie preferences, should we add analytics
        return Page();
    }

    public IActionResult OnPostHideBanner()
    {
        // todo - only grant consent on approval if analytics involved
        Response.Cookies.Append(ConsentCookieName, "hide");
        var referrer = Request.Headers.Referer.ToString();
        return Redirect(referrer);
    }
}
