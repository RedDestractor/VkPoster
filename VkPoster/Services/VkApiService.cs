using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkPoster.Interfaces;
using VkPoster.VkApiHelpers;
using VkNet.Model;
using VkNet.Utils;
using VkPoster.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using System.Windows.Media.Imaging;

namespace VkPoster.Services
{
    public class VkApiService : IVkApiService
    {
        public List<GroupDto> GetGroups()
        {
            var vkApi = VkApiSingleton.GetIntance;
            var groupsDtoList = new List<GroupDto>();
            var groups = vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Count = 10, Fields = GroupsFields.Description});
            foreach (var group in groups)
            {
                var groupDto = new GroupDto();
                groupDto.Name = group.Name;
                groupDto.Image = new BitmapImage(group.Photo50);
                groupDto.Description = group.Description;
                groupsDtoList.Add(groupDto);
            }

            return groupsDtoList;
        }
    }
}
