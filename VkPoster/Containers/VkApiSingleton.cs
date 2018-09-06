using VkNet;

namespace VkPoster.Containers
{
    public static class VkApiSingleton
    {
        private static VkApi _vkApiInstance;
        public static VkApi GetInstance => _vkApiInstance ?? (_vkApiInstance = new VkApi());
    }
}
