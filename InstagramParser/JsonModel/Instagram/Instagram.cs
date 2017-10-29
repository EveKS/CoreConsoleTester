using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramParser.JsonModel.Instagram
{
    public class Config
    {

        [JsonProperty("csrf_token")]
        public string CsrfToken { get; set; }

        [JsonProperty("viewer")]
        public object Viewer { get; set; }
    }

    public class FollowedBy
    {

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Follows
    {

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Dimensions
    {

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class Owner
    {

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Comments
    {

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Likes
    {

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Node
    {

        [JsonProperty("__typename")]
        public string Typename { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("comments_disabled")]
        public bool CommentsDisabled { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonProperty("gating_info")]
        public object GatingInfo { get; set; }

        [JsonProperty("media_preview")]
        public string MediaPreview { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("thumbnail_src")]
        public string ThumbnailSrc { get; set; }

        [JsonProperty("thumbnail_resources")]
        public IList<object> ThumbnailResources { get; set; }

        [JsonProperty("is_video")]
        public bool IsVideo { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("display_src")]
        public string DisplaySrc { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("comments")]
        public Comments Comments { get; set; }

        [JsonProperty("likes")]
        public Likes Likes { get; set; }

        [JsonProperty("video_views")]
        public int? VideoViews { get; set; }
    }

    public class PageInfo
    {

        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }

        [JsonProperty("end_cursor")]
        public string EndCursor { get; set; }
    }

    public class Media
    {

        [JsonProperty("nodes")]
        public IList<Node> Nodes { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page_info")]
        public PageInfo PageInfo { get; set; }
    }

    public class User
    {

        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("blocked_by_viewer")]
        public bool BlockedByViewer { get; set; }

        [JsonProperty("country_block")]
        public bool CountryBlock { get; set; }

        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }

        [JsonProperty("external_url_linkshimmed")]
        public string ExternalUrlLinkshimmed { get; set; }

        [JsonProperty("followed_by")]
        public FollowedBy FollowedBy { get; set; }

        [JsonProperty("followed_by_viewer")]
        public bool FollowedByViewer { get; set; }

        [JsonProperty("follows")]
        public Follows Follows { get; set; }

        [JsonProperty("follows_viewer")]
        public bool FollowsViewer { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("has_blocked_viewer")]
        public bool HasBlockedViewer { get; set; }

        [JsonProperty("has_requested_viewer")]
        public bool HasRequestedViewer { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [JsonProperty("profile_pic_url_hd")]
        public string ProfilePicUrlHd { get; set; }

        [JsonProperty("requested_by_viewer")]
        public bool RequestedByViewer { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("connected_fb_page")]
        public object ConnectedFbPage { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }
    }

    public class ProfilePage
    {

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("logging_page_id")]
        public string LoggingPageId { get; set; }
    }

    public class EntryData
    {

        [JsonProperty("ProfilePage")]
        public IList<ProfilePage> ProfilePage { get; set; }
    }

    public class Gatekeepers
    {

        [JsonProperty("bn")]
        public bool Bn { get; set; }

        [JsonProperty("ld")]
        public bool Ld { get; set; }

        [JsonProperty("pl")]
        public bool Pl { get; set; }
    }

    public class P
    {

        [JsonProperty("use_new_styles")]
        public string UseNewStyles { get; set; }
    }

    public class Ebd
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Bc3l
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Ccp
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class CreateUpsell
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Disc
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Feed
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class SuUniverse
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Us
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class UsLi
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Nav
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class NavLo
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Profile
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Deact
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Sidecar
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Video
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Filters
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Typeahead
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class LocationTag
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class PwLink
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class DeltaDefaults
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Appsell
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class ProfileSensitivity
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Save
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Stale
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Reg
    {

        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("p")]
        public P P { get; set; }
    }

    public class Qe
    {

        [JsonProperty("ebd")]
        public Ebd Ebd { get; set; }

        [JsonProperty("bc3l")]
        public Bc3l Bc3l { get; set; }

        [JsonProperty("ccp")]
        public Ccp Ccp { get; set; }

        [JsonProperty("create_upsell")]
        public CreateUpsell CreateUpsell { get; set; }

        [JsonProperty("disc")]
        public Disc Disc { get; set; }

        [JsonProperty("feed")]
        public Feed Feed { get; set; }

        [JsonProperty("su_universe")]
        public SuUniverse SuUniverse { get; set; }

        [JsonProperty("us")]
        public Us Us { get; set; }

        [JsonProperty("us_li")]
        public UsLi UsLi { get; set; }

        [JsonProperty("nav")]
        public Nav Nav { get; set; }

        [JsonProperty("nav_lo")]
        public NavLo NavLo { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("deact")]
        public Deact Deact { get; set; }

        [JsonProperty("sidecar")]
        public Sidecar Sidecar { get; set; }

        [JsonProperty("video")]
        public Video Video { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }

        [JsonProperty("typeahead")]
        public Typeahead Typeahead { get; set; }

        [JsonProperty("location_tag")]
        public LocationTag LocationTag { get; set; }

        [JsonProperty("pw_link")]
        public PwLink PwLink { get; set; }

        [JsonProperty("delta_defaults")]
        public DeltaDefaults DeltaDefaults { get; set; }

        [JsonProperty("appsell")]
        public Appsell Appsell { get; set; }

        [JsonProperty("profile_sensitivity")]
        public ProfileSensitivity ProfileSensitivity { get; set; }

        [JsonProperty("save")]
        public Save Save { get; set; }

        [JsonProperty("stale")]
        public Stale Stale { get; set; }

        [JsonProperty("reg")]
        public Reg Reg { get; set; }
    }

    public class DisplayPropertiesServerGuess
    {

        [JsonProperty("pixel_ratio")]
        public double PixelRatio { get; set; }

        [JsonProperty("viewport_width")]
        public int ViewportWidth { get; set; }
    }

    public class Instagram
    {

        [JsonProperty("activity_counts")]
        public object ActivityCounts { get; set; }

        [JsonProperty("config")]
        public Config Config { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }

        [JsonProperty("entry_data")]
        public EntryData EntryData { get; set; }

        [JsonProperty("gatekeepers")]
        public Gatekeepers Gatekeepers { get; set; }

        [JsonProperty("qe")]
        public Qe Qe { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("display_properties_server_guess")]
        public DisplayPropertiesServerGuess DisplayPropertiesServerGuess { get; set; }

        [JsonProperty("environment_switcher_visible_server_guess")]
        public bool EnvironmentSwitcherVisibleServerGuess { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("is_canary")]
        public bool IsCanary { get; set; }

        [JsonProperty("probably_has_app")]
        public bool ProbablyHasApp { get; set; }

        [JsonProperty("show_app_install")]
        public bool ShowAppInstall { get; set; }
    }
}
