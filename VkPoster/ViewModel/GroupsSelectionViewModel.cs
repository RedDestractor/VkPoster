using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using VkPoster.Interfaces;
using VkPoster.Model;
using VkNet.Model;
using VkNet.Utils;
using System.Collections.Generic;

namespace VkPoster.ViewModel
{
    public class GroupsSelectionViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IFrameNavigationService _navigationService;
        private readonly IVkApiService _vkApiService;

        public const string WelcomeTitlePropertyName = "Vk Poster";

        private string welcomeTitle = string.Empty;
        private object selectedViewModel;

        public List<GroupDto> Groups;

        public string WelcomeTitle
        {
            get
            {
                return welcomeTitle;
            }
            set
            {
                Set(ref welcomeTitle, value);
            }
        }

        public object SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }
            set
            {
                selectedViewModel = value;
                RaisePropertyChanged(() => SelectedViewModel);
            }
        }


        public GroupsSelectionViewModel(IDataService dataService, IFrameNavigationService navigationService, IVkApiService vkApiService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _vkApiService = vkApiService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
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

        public RelayCommand OnLoadedGetGroupsCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        Groups = _vkApiService.GetGroups();
                    });
            }
        }
    }
}