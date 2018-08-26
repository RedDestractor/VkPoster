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

namespace VkPoster.Services
{
    public class VkApiService : IVkApiService
    {
        public List<GroupDto> GetGroups()
        {
            var vkApi = VkApiSingleton.GetIntance;
            var groupsDtoList = new List<GroupDto>();
            var groups = vkApi.Groups.Get(new VkNet.Model.RequestParams.GroupsGetParams() { UserId = 1, Count = 10 });
            foreach (var group in groups)
            {
                var groupDto = new GroupDto();
                groupDto.Cover = group.Cover;
                groupDto.Description = group.Description;
                groupsDtoList.Add(groupDto);
            }

            return groupsDtoList;
        }
    }
}
