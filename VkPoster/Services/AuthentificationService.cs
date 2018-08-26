using GalaSoft.MvvmLight.Ioc;
using System;
using System.Web;
using System.Windows.Controls;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;
using VkPoster.Constants;
using VkPoster.ViewModel;
using VkPoster.VkApiHelpers;

namespace VkPoster.Service
{
    public class AuthentificationService : IAuthService
    {
        private const string AppId = "6495092";
        private const string Scope = "270336";

        private static readonly VkApi VkApi = VkApiSingleton.GetIntance;

        public void GetOauthPage(WebBrowser webBrowser)
        {
            var url = "https://oauth.vk.com/authorize?client_id=" + AppId + "&scope" + Scope +
                      "redirect_uri=https://oauth.vk.com/blank.html&display=popup&response_type=token";

            webBrowser.Navigate(url);
        }

        public bool Authentificate(string url)
        {
            if (url.Contains("access_token") && url.Contains("#"))
            {
                url = (new System.Text.RegularExpressions.Regex("#")).Replace(url, "?", 1);
                PrivateInfo.Token = HttpUtility.ParseQueryString(url).Get("access_token");

                VkApi.Authorize(new ApiAuthParams
                {
                    AccessToken = PrivateInfo.Token
                });
                return true;
            }
            return false;
        }

        public void DeleteCookie(Uri url)
        {
            string cookie = string.Empty;
            try
            {
                cookie = System.Windows.Application.GetCookie(url);
            }
            catch (Exception ex)
            {

            }
            if (!string.IsNullOrEmpty(cookie))
            {
                var values = cookie.Split(';');
                foreach (var s in values)
                {
                    if (s.IndexOf('=') > 0)
                    {
                        DeleteSingleCookie(s.Substring(0, s.IndexOf('=')).Trim(), url);
                    }
                }
            }
        }

        private void DeleteSingleCookie(string name, Uri url)
        {
            try
            {
                var expiration = DateTime.UtcNow - TimeSpan.FromDays(1);
                string cookie = String.Format("{0}=; expires={1}; path=/; domain=.vk.com", name, expiration.ToString("R"));
                System.Windows.Application.SetCookie(url, cookie);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

