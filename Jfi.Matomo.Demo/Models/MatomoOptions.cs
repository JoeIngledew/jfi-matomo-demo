namespace Jfi.Matomo.Demo.Models;

public class MatomoOptions
{
    public string? TrackerUrl { get; set; } = string.Empty;

    public int SiteId { get; set; }

    public MatomoTrackerOptions TrackerOptions { get; set; } = new MatomoTrackerOptions();
}

public class MatomoTrackerOptions
{
    public bool DisableCookieTimeoutExtension { get; set; } = true;

    public bool NoScriptTracking { get; set; } = true;

    public bool PrependDomainToTitle { get; set; } = false;

    public bool ClientDoNotTrackDetection { get; set; } = true;

    public bool RequireConsent { get; set; } = true;

    public bool RequireCookieConsent { get; set; } = true;
}
