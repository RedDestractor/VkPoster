using System.Collections.Generic;
using VkPoster.Interfaces;
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
using VkPoster.Containers;

namespace VkPoster.Helpers
{
    public class VkApiWorker
    {
        public Queue<GroupDto> GroupsToGetPosts { get; set; }
        public GroupDto AdminGroupToPost { get; set; }

        private readonly VkApi _vkApi;
        private readonly List<Post> _lastPosted;

        public VkApiWorker(GroupsSelectionViewModel ctx)
        {
            _vkApi = Api.GetInstance();
            _lastPosted = new List<Post>();
        }

        public List<GroupDto> GetGroups(bool isAdminOnly = false)
        {
            var groupsDtoList = new List<GroupDto>();

            var groups = isAdminOnly
                ? _vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Filter = GroupsFilters.Administrator, Fields = GroupsFields.All })
                : _vkApi.Groups.Get(new GroupsGetParams() { Extended = true, Filter = GroupsFilters.Publics, Fields = GroupsFields.All });
            foreach (var group in groups)
            {
                var groupDto = new GroupDto { Name = @group.Name };

                var src = new BitmapImage();
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
            var attachments = new List<MediaAttachment>();
            var wallPostsParam = new WallPostParams();
            var isOnPost = false;

            foreach (var source in post.Attachments)
            {
                if (source.Instance is MediaAttachment attachment)
                {
                    attachments.Add(attachment);
                }
            }
            
            if (attachments.Count > 0)
            {
                wallPostsParam.Attachments = attachments;
                isOnPost = true;
            }
            if (!string.IsNullOrEmpty(post.Text))
            {
                wallPostsParam.Message = post.Text;
                isOnPost = true;
            }
            wallPostsParam.OwnerId = -AdminGroupToPost.Id;
            wallPostsParam.FromGroup = true;

            if(isOnPost)
                _vkApi.Wall.Post(wallPostsParam);
        }

        private Post GetPost()
        {
            var groupToGetPost = GroupsToGetPosts.Dequeue();

            var groupData = _vkApi.Wall.Get(new WallGetParams
            {
                OwnerId = -groupToGetPost.Id,
                Count = 15,
                Extended = true
            }).WallPosts;

            var post = groupData
                .Where(x => !_lastPosted
                .Select(y => y.Id)
                .Contains(x.Id))
                .FirstOrDefault();

            _lastPosted.Add(post);

            GroupsToGetPosts.Enqueue(groupToGetPost);

            return post;
        }
    }
}
