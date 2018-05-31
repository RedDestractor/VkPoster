using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VkPoster.Model;
using VkPoster.ViewModel;

namespace VkPoster.Service
{
    public class AuthentificationService : IAuthService
    {
        private const string AppId = "6495092";
        private const string Scope = "270336";

        public void GetOauthPage(WebBrowser webBrowser)
        {
            var url = "https://oauth.vk.com/authorize?client_id=" + AppId + "&scope" + Scope + 
                      "redirect_uri=https://oauth.vk.com/blank.html&display=popup&response_type=token";

            webBrowser.Navigate(url);
        }

        public void DeleteCookie(Uri url)
        {
            string cookie = string.Empty;
            try
            {
                cookie = Application.GetCookie(url);
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
                Application.SetCookie(url, cookie);
            }
            catch (Exception exc)
            {

            }
        }
    }
}
