namespace Jfi.Matomo.Demo.Pages.Shared
{
    using Jfi.Matomo.Demo.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class MatomoPartialModel : PageModel
    {
        private readonly MatomoOptions _options;

        public MatomoPartialModel(MatomoOptions options)
        {
            _options = options;
        }

        private const string DisableCookieTimeoutExtension = @"
            _paq.push([function() {
              var self = this;
              function getOriginalVisitorCookieTimeout() {
                var now = new Date(),nowTs = Math.round(now.getTime() / 1000),visitorInfo = self.getVisitorInfo();
                var createTs = parseInt(visitorInfo[2]);
                var cookieTimeout = 33696000;
                var originalTimeout = createTs + cookieTimeout - nowTs;
                return originalTimeout;
              }
              this.setVisitorCookieTimeout( getOriginalVisitorCookieTimeout() );
            }]);
        ";
        private const string PrependDomainToTitle = @"_paq.push(['setDocumentTitle', document.domain + '/' + document.title]);";
        private const string ClientDoNotTrackDetection = @"_paq.push(['setDoNotTrack', true]);";
        private const string RequireConsent = @"_paq.push(['requireConsent']);";
        private const string RequireCookieConsent = @"_paq.push(['requireCookieConsent']);";
        private const string HasConsent = @"_paq.push(['setConsentGiven']);";
        private const string HasCookieConsent = @"_paq.push(['setCookieConsentGiven']);";

        public string DisableCookieTimeoutExtensionScript { get; set; } = string.Empty;
        public string PrependDomainToTitleScript { get; set; } = string.Empty;
        public string ClientDoNotTrackDetectionScript { get; set; } = string.Empty;
        public string RequireConsentScript { get; set; } = string.Empty;
        public string RequireCookieConsentScript { get; set; } = string.Empty;
        public string HasConsentScript { get; set; } = string.Empty;
        public string HasCookieConsentScript { get; set; } = string.Empty;
        public string? TrackerUrl { get; set; } = string.Empty;
        public int SiteId { get; set; } = 0;
        public bool DoNotRender { get; set; } = false;
        public Uri? NoScriptTrackingImgSrc { get; set; }

        public void OnGet()
        {
            TrackerUrl = _options.TrackerUrl;
            SiteId = _options.SiteId;

            if (string.IsNullOrEmpty(TrackerUrl) || SiteId == 0)
            {
                DoNotRender = true;
                return;
            }

            string? consentCookieValue = null;
            Request.Cookies.TryGetValue(CookiesModel.ConsentCookieName, out consentCookieValue);
            bool hasConsent = !string.IsNullOrEmpty(consentCookieValue) && string.Equals(consentCookieValue, "accept");

            DisableCookieTimeoutExtensionScript = _options.TrackerOptions.DisableCookieTimeoutExtension
                ? DisableCookieTimeoutExtension : string.Empty;
            PrependDomainToTitleScript = _options.TrackerOptions.PrependDomainToTitle
                ? PrependDomainToTitle : string.Empty;
            ClientDoNotTrackDetectionScript = _options.TrackerOptions.ClientDoNotTrackDetection
                ? ClientDoNotTrackDetection : string.Empty;
            RequireConsentScript = _options.TrackerOptions.RequireConsent
                ? RequireConsent : string.Empty;
            RequireCookieConsentScript = _options.TrackerOptions.RequireCookieConsent
                ? RequireCookieConsent : string.Empty;
            HasConsentScript = hasConsent
                ? HasConsent : string.Empty;
            HasCookieConsentScript = hasConsent
                ? HasCookieConsent : string.Empty;

            if (hasConsent && _options.TrackerOptions.NoScriptTracking)
                NoScriptTrackingImgSrc = new Uri(new Uri(TrackerUrl), $"matomo.php?idsite={SiteId}&rec=1");
        }
    }
}
