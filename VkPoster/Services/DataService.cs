using System;
using VkPoster.Interfaces;
using VkPoster.Model;

namespace VkPoster.Services
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            var item = new DataItem("Welcome to VkPoster!");
            callback(item, null);
        }
    }
}