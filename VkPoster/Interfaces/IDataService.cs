using System;
using VkPoster.Model;

namespace VkPoster.Interfaces
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
