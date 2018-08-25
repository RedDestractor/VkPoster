using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using test.Interfaces;
using VkPoster.Model;

namespace VkPoster.ViewModel
{
    public class WorkViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IFrameNavigationService _navigationService;

        public const string WelcomeTitlePropertyName = "Vk Poster";

        private string welcomeTitle = string.Empty;
        private object selectedViewModel;

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

        public WorkViewModel(IDataService dataService, IFrameNavigationService navigationService)
        {
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
    }
}