namespace Jfi.Matomo.Demo.Pages.Shared.Components.MatomoScript;

using Jfi.Matomo.Demo.Models;

using Microsoft.AspNetCore.Mvc;


public class MatomoScriptViewComponent : ViewComponent
{
    private readonly MatomoOptions _options;

    public MatomoScriptViewComponent(MatomoOptions options)
    {
        _options = options;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new MatomoScriptComponentModel();

        model.TrackerUrl = _options.TrackerUrl;
        model.SiteId = _options.SiteId;

        if (string.IsNullOrEmpty(model.TrackerUrl) || model.SiteId == 0)
        {
            model.DoNotRender = true;
            return await Task.FromResult(View(model));
        }

        Request.Cookies.TryGetValue(CookiesModel.ConsentCookieName, out string? consentCookieValue);
        bool hasConsent = !string.IsNullOrEmpty(consentCookieValue) && string.Equals(consentCookieValue, "accept");

        model.DisableCookieTimeoutExtension = _options.TrackerOptions.DisableCookieTimeoutExtension;
        model.PrependDomainToTitle = _options.TrackerOptions.PrependDomainToTitle;
        model.ClientDoNotTrackDetection = _options.TrackerOptions.ClientDoNotTrackDetection;
        model.RequireConsent = _options.TrackerOptions.RequireConsent;
        model.RequireCookieConsent = _options.TrackerOptions.RequireCookieConsent;
        model.HasConsent = hasConsent;
        model.HasCookieConsent = hasConsent;
        model.IncludeNoScriptTrackingImg = hasConsent;

        if (hasConsent && _options.TrackerOptions.NoScriptTracking)
            model.NoScriptTrackingImgSrc = new Uri(new Uri(model.TrackerUrl), $"matomo.php?idsite={model.SiteId}&rec=1");

        return await Task.FromResult(View(model));
    }
}

public class MatomoScriptComponentModel
{
    public string? TrackerUrl { get; set; } = string.Empty;
    public int SiteId { get; set; } = 0;
    public bool DoNotRender { get; set; } = false;
    public Uri? NoScriptTrackingImgSrc { get; set; }

    public bool DisableCookieTimeoutExtension { get; set; } = false;
    public bool PrependDomainToTitle { get; set; } = false;
    public bool ClientDoNotTrackDetection { get; set; } = false;
    public bool RequireConsent { get; set; } = false;
    public bool RequireCookieConsent { get; set; } = false;
    public bool HasConsent { get; set; } = false;
    public bool HasCookieConsent { get; set; } = false;
    public bool IncludeNoScriptTrackingImg { get; set; }
}