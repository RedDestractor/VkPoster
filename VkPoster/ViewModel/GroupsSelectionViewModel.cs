using GalaSoft.MvvmLight;
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
        public const string WelcomeTitlePropertyName = "Vk Poster";

        private readonly IDataService _dataService;
        private readonly IFrameNavigationService _navigationService;
        private readonly VkApiWorker _vkApi;

        private string _welcomeTitle = string.Empty;
        private int _timeforExecution;
        private object _selectedViewModel;
        private ObservableCollection<GroupDto> _groups;
        private ObservableCollection<GroupDto> _adminGroups;

        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        public int TimeForExecution
        {
            get
            {
                return _timeforExecution;
            }
            set
            {
                Set(ref _timeforExecution, value);
            }
        }

        public object SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged(() => SelectedViewModel);
            }
        }

        public ObservableCollection<GroupDto> GroupsCollection
        {
            get
            {
                return _groups;
            }
            set
            {                
                Set(ref _groups, value);
            }
        }

        public ObservableCollection<GroupDto> AdminGroupsCollection
        {
            get
            {
                return _adminGroups;
            }
            set
            {
                Set(ref _adminGroups, value);
            }
        }

        public GroupsSelectionViewModel(IDataService dataService, IFrameNavigationService navigationService)
        {
            _vkApi = new VkApiWorker(this);
            _navigationService = navigationService;
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
            var tmpGroups = _vkApi.GetGroups();
            var tmpAdminGroups = _vkApi.GetGroups(true);
            GroupsCollection = new ObservableCollection<GroupDto>(tmpGroups);
            AdminGroupsCollection = new ObservableCollection<GroupDto>(tmpAdminGroups);
            TimeForExecution = 15; 
        }

        public RelayCommand HomeViewNavigationCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("HomeView");
                    });
            }
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

        public RelayCommand SetTimeViewNavigateComand
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