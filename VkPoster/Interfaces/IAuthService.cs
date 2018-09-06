using System;
using System.Windows.Controls;

namespace VkPoster.Interfaces
{
    public interface IAuthService
    {
        void GetOauthPage(WebBrowser webBrowser);
        void DeleteCookie(Uri url);
        bool Authenticate(string url);
    }
}