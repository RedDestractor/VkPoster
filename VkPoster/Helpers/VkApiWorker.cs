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
using System.Collections.ObjectModel;
using VkNet;
using GalaSoft.MvvmLight.Ioc;
using VkPoster.ViewModel;
using GalaSoft.MvvmLight;

namespace VkPoster.Helpers
{
    //TODO
    //set propertie from another class when propertie changed mvvm C#
    //https://stackoverflow.com/questions/19902329/how-to-set-a-onpropertychanged-method-from-another-class

    public class VkApiWorker
    {
        private VkApi vkApi;
        private GroupsSelectionViewModel context;

        public Queue<GroupDto> groupsToGetPosts;
        public Queue<GroupDto> adminGroupsToPost;

        public VkApiWorker(GroupsSelectionViewModel ctx)
        {
            vkApi = VkApiSingleton.GetIntance;
            context = ctx;
        }

        public List<GroupDto> GetGroups(bool IsAdminOnly = false)
        {            
            var groupsDtoList = new List<GroupDto>();

            VkCollection<Group> groups;
            if (IsAdminOnly)
                groups = vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Filter = GroupsFilters.Administrator, Fields = GroupsFields.All });
            else
                groups = vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Filter = GroupsFilters.Publics, Fields = GroupsFields.All });

            foreach (var group in groups)
            {
                var groupDto = new GroupDto();
                groupDto.Name = group.Name;

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = group.Photo200;
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();

                groupDto.Id = group.Id;
                groupDto.Image = src;
                groupDto.Description = group.Description;
                groupDto.IsAdmin = group.IsAdmin;
                groupsDtoList.Add(groupDto);
            }

            return groupsDtoList;
        }

        //public void SetGroupsToGetPosts()
        //{
        //    GroupsToGetPosts = new Queue<GroupDto>(context.GroupsCollection.Where(x => x.IsSelected == true));
        //}
        //public void SetAdminGroupsToPost()
        //{
        //    AdminGroupsToPost = new Queue<GroupDto>(context.AdminGroupsCollection.Where(x => x.IsSelected == true));
        //}

        //public WallGetObject GetPost()
        //{
            //var groupToGetPost = GroupsToGetPosts.Dequeue();

            //var groupData = vkApi.Wall.Get(new WallGetParams
            //{
            //    OwnerId = groupToGetPost.Id,
            //    Count = 10,
            //    Extended = true            
            //});

            //GroupsToGetPosts.Enqueue(groupToGetPost);

        //    return groupData;
        //}        
    }
}
