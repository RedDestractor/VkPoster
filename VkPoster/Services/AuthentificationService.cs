using System;
using System.Web;
using System.Windows.Controls;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkPoster.Constants;
using VkPoster.Containers;
using VkPoster.Interfaces;

namespace VkPoster.Services
{
    public class AuthenticationService : IAuthService
    {
        private const string AppId = "6495092";
        private const string Scope = "wall,offline,groups,photos,manage";

        private static readonly VkApi VkApi = VkApiSingleton.GetInstance;

        public void GetOauthPage(WebBrowser webBrowser)
        {
            webBrowser.Navigate("https://oauth.vk.com/authorize?client_id=" + AppId + "&scope" + Scope +
                               "redirect_uri=https://oauth.vk.com/blank.html&response_type=token");
        }

        public bool Authenticate(string url)
        {
            if (!url.Contains("access_token") || !url.Contains("#")) return false;
            url = (new System.Text.RegularExpressions.Regex("#")).Replace(url, "?", 1);
            PrivateInfo.Token = HttpUtility.ParseQueryString(url).Get("access_token");

            VkApi.Authorize(new ApiAuthParams
            {
                ApplicationId = 6495092,
                AccessToken = PrivateInfo.Token,
                Settings = Settings.Offline
            });
            return true;
        }

        public void DeleteCookie(Uri url)
        {
            var cookie = string.Empty;
            try
            {
                cookie = System.Windows.Application.GetCookie(url);
            }
            catch (Exception)
            {
                // ignored
            }

            if (string.IsNullOrEmpty(cookie)) return;
            var values = cookie.Split(';');
            foreach (var s in values)
            {
                if (s.IndexOf('=') > 0)
                {
                    DeleteSingleCookie(s.Substring(0, s.IndexOf('=')).Trim(), url);
                }
            }
        }

        private static void DeleteSingleCookie(string name, Uri url)
        {
            try
            {
                var expiration = DateTime.UtcNow - TimeSpan.FromDays(1);
                var cookie = $"{name}=; expires={expiration:R}; path=/; domain=.vk.com";
                System.Windows.Application.SetCookie(url, cookie);
            }
            catch (Exception)
            {

            }
        }
    }
}

