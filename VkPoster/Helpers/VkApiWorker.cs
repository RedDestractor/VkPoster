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
using VkNet.Model.Attachments;

namespace VkPoster.Helpers
{
    public class VkApiWorker
    {
        private VkApi vkApi;

        public Queue<GroupDto> GroupsToGetPosts { get; set; }
        public GroupDto AdminGroupToPost { get; set; }

        public VkApiWorker(GroupsSelectionViewModel ctx)
        {
            vkApi = VkApiSingleton.GetIntance;
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

        public void SetPostToAdminGroup()
        {
            var post = GetPost();

            List<MediaAttachment> attachments = new List<MediaAttachment>();

            foreach(var source in post.Attachments)
            {
                attachments.Add(source.Instance as MediaAttachment);
            }

            vkApi.Wall.Post(new WallPostParams()
            {
                OwnerId = -AdminGroupToPost.Id,
                //Attachments = attachments
                Message = post.Text
            });
        }

        private Post GetPost()
        {
            var groupToGetPost = GroupsToGetPosts.Dequeue();

            var groupData = vkApi.Wall.Get(new WallGetParams
            {
                OwnerId = -groupToGetPost.Id,
                Count = 10,
                Extended = true
            }).WallPosts;

            var post = groupData.FirstOrDefault();

            GroupsToGetPosts.Enqueue(groupToGetPost);

            return post;
        }        
    }
}
