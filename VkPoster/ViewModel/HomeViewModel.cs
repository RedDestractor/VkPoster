using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using VkPoster.Interfaces;
using VkPoster.Model;

namespace VkPoster.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IFrameNavigationService _navigationService;
        private string _welcomeTitle = string.Empty;
        private string _login = "LOGIN";
        private string _password = "PASSWORD";
        private object _selectedViewModel;        

        public string WelcomeTitle
        {
            get => _welcomeTitle;
            set => Set(ref _welcomeTitle, value);
        }

        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
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

        public HomeViewModel(IDataService dataService, IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
            dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
        }

        public RelayCommand ShowAuthenticationViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send(new NotificationMessage("ConnectToVk"));
                });
            }
        }

        public RelayCommand CloseAuthenticationViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send(new NotificationMessage("ConnectionToVkFinished"));
                });
            }
        }

        public RelayCommand WorkViewNavigationCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("GroupsSelectionView");
                    });
            }
        }
    }
}