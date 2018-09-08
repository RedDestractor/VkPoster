using Microsoft.Extensions.DependencyInjection;
using VkNet;

namespace VkPoster.Containers
{
	public class Api
    {
        private static VkApi _instance;

        private Api()
        { }

        public static VkApi GetInstance(IServiceCollection collection)
        {
            return _instance ?? (_instance = new VkApi(collection));
        }

        public static VkApi GetInstance()
        {
            return _instance ?? (_instance = new VkApi());
        }
    }
}
