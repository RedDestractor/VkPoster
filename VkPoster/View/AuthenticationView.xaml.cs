using GalaSoft.MvvmLight.Ioc;
using System;
using System.Web;
using System.Windows;
using VkPoster.Containers;
using VkPoster.Interfaces;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class AuthenticationView : Window
    {
        private readonly IAuthService _authenticationService;
        bool _isConnectionFinished;

        public AuthenticationView()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();

            _authenticationService = SimpleIoc.Default.GetInstance<IAuthService>();
            _authenticationService.DeleteCookie(new Uri("https://www.vk.com"));
            _authenticationService.GetOauthPage(webBrowser);

            webBrowser.LoadCompleted += LoadCompletedEvent;
        }

        private void LoadCompletedEvent(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                _isConnectionFinished = _authenticationService.Authenticate(e.Uri.Fragment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (_isConnectionFinished)
            {
                var viewModel = (HomeViewModel) DataContext;
                viewModel.CloseAuthenticationViewCommand.Execute(null);
                viewModel.WorkViewNavigationCommand.Execute(null);
            }
        }
    }
}
