using GalaSoft.MvvmLight.Ioc;
using System;
using System.Web;
using System.Windows;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class AuthentificationView : Window
    {
        private readonly IAuthService _authentificationService;
        bool IsConnectionFinished;

        public AuthentificationView()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();

            _authentificationService = SimpleIoc.Default.GetInstance<IAuthService>();
            _authentificationService.DeleteCookie(new Uri("https://www.vk.com"));
            _authentificationService.GetOauthPage(webBrowser);

            webBrowser.LoadCompleted += LoadCompletedEvent;
        }

        private void LoadCompletedEvent(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                IsConnectionFinished = _authentificationService.Authentificate(e.Uri.Fragment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (IsConnectionFinished)
            {
                var viewModel = (HomeViewModel)DataContext;
                viewModel.CloseAuthentificationViewCommand.Execute(null);
                viewModel.WorkViewNavigationCommand.Execute(null);
            }
        }
    }
}
