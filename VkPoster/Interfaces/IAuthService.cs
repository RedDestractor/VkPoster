using System;
using System.Windows.Controls;
using VkPoster.Model;

namespace VkPoster.ViewModel
{
    public interface IAuthService
    {
        void GetOauthPage(WebBrowser webBrowser);
        void DeleteCookie(Uri url);
    }
}