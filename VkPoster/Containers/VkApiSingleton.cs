using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace VkPoster.VkApiHelpers
{
    public static class VkApiSingleton
    {
        private static VkApi _vkApiInstance;
        public static VkApi GetIntance
        {
            get
            {
                if (_vkApiInstance == null)
                {
                    _vkApiInstance = new VkApi();
                }
                return _vkApiInstance;
            }
        }
    }
}
