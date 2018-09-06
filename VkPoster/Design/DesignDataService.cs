using System;
using VkPoster.Interfaces;
using VkPoster.Model;

namespace VkPoster.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to VkPoster!");
            callback(item, null);
        }
    }
}