using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Utils;
using VkNet.Wpf;
using VkPoster.Containers;
using VkPoster.Interfaces;
using VkPoster.Model;

namespace VkPoster.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IFrameNavigationService _navigationService;
        private string _welcomeTitle = string.Empty;
        private object _selectedViewModel;
        private VkApi _api;

        public string WelcomeTitle
        {
            get => _welcomeTitle;
            set => Set(ref _welcomeTitle, value);
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

        public HomeViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
            _api = Api.GetInstance(InitDi());
        }

        public RelayCommand ShowAuthenticationViewCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        WorkViewNavigation();
                    });
            }
        }

        private void WorkViewNavigation()
        {
           if (_api.IsAuthorized)
           {
                return;
           }

            _api.Authorize(new ApiAuthParams
            {
                ApplicationId = 6495092,
                Settings = Settings.All
            });

            _navigationService.NavigateTo("GroupsSelectionView");
        }

        private static ServiceCollection InitDi()
        {
            var di = new ServiceCollection();

            di.AddSingleton<IBrowser, WpfAuthorize>();

            return di;
        }
    }
}