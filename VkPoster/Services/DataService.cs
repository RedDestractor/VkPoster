using System;

namespace VkPoster.Model
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