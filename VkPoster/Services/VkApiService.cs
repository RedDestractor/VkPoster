using System.Collections.Generic;
using VkPoster.Interfaces;
using VkPoster.VkApiHelpers;
using VkPoster.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using System.Windows.Media.Imaging;
using System;
using System.Linq;
using VkNet.Utils;
using VkNet.Model;

namespace VkPoster.Services
{
    public class VkApiService : IVkApiService
    {
        public List<GroupDto> GetGroups(bool IsAdmin)
        {
            var vkApi = VkApiSingleton.GetIntance;
            var groupsDtoList = new List<GroupDto>();

            VkCollection<Group> groups;
            if (IsAdmin)
                groups = vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Filter = GroupsFilters.Administrator, Fields = GroupsFields.All });
            else
            {
                groups = vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Filter = GroupsFilters.All, Fields = GroupsFields.All });

            }

            foreach (var group in groups)
            {
                var groupDto = new GroupDto();
                groupDto.Name = group.Name;

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = group.Photo200;
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();

                groupDto.Image = src;
                groupDto.Description = group.Description;
                groupDto.IsAdmin = group.IsAdmin;
                groupsDtoList.Add(groupDto);
            }

            return groupsDtoList;
        }
    }
}
