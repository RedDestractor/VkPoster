using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VkPoster.Model;

namespace VkPoster.Service
{
    public class AuthentificationService
    {
        private const string AppId = "6495092";
        private const string Scope = "270336";

        public UserInfo GetUserInfo()
        {
            var url = "https://oauth.vk.com/authorize?client_id=" + AppId + "&scope" + Scope + "redirect_uri= https://oauth.vk.com/blank.html";

            throw new NotImplementedException();
        }

    }
}
