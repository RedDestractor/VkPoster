using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
    }
}
