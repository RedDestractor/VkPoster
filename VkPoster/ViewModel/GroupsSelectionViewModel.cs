﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using VkPoster.Interfaces;
using VkPoster.Model;
using VkNet.Model;
using VkNet.Utils;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using VkPoster.Helpers;
using System.Linq;

namespace VkPoster.ViewModel
{
    public class GroupsSelectionViewModel : ViewModelBase
    {
        private readonly IFrameNavigationService _navigationService;
        private readonly VkApiWorker _vkApi;

        private string _welcomeTitle = string.Empty;
        private int _timeForExecution;
        private object _selectedViewModel;
        private ObservableCollection<GroupDto> _groups;
        private ObservableCollection<GroupDto> _adminGroups;

        public string WelcomeTitle
        {
            get => _welcomeTitle;
            set => Set(ref _welcomeTitle, value);
        }

        public int TimeForExecution
        {
            get => _timeForExecution;
            set => Set(ref _timeForExecution, value);
        }

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged(() => SelectedViewModel);
            }
        }

        public ObservableCollection<GroupDto> GroupsCollection
        {
            get => _groups;
            set => Set(ref _groups, value);
        }

        public ObservableCollection<GroupDto> AdminGroupsCollection
        {
            get => _adminGroups;
            set => Set(ref _adminGroups, value);
        }

        public GroupsSelectionViewModel(IFrameNavigationService navigationService)
        {
            _vkApi = new VkApiWorker(this);
            _navigationService = navigationService;

            var tmpGroups = _vkApi.GetGroups();
            var tmpAdminGroups = _vkApi.GetGroups(true);

            GroupsCollection = new ObservableCollection<GroupDto>(tmpGroups);
            AdminGroupsCollection = new ObservableCollection<GroupDto>(tmpAdminGroups);
            TimeForExecution = 15; 
        }

        public RelayCommand AdminGroupsViewNavigationCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo("AdminGroupsSelectionView");
                    _vkApi.GroupsToGetPosts = new Queue<GroupDto>(GroupsCollection.Where(x => x.IsSelected == true));
                });
            }
        }

        public RelayCommand SetTimeViewNavigateCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo("SetTimeView");
                    _vkApi.AdminGroupToPost = AdminGroupsCollection.FirstOrDefault(x => x.IsSelected == true);
                });
            }
        }

        public RelayCommand ProgressViewNavigationCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo("ProgressView");
                });
            }
        }

        public RelayCommand SetPostToAdminGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _vkApi.SetPostToAdminGroup();
                });
            }
        }
    }
}