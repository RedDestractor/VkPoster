using GalaSoft.MvvmLight.Ioc;
using System;
using System.Web;
using System.Windows;
using VkPoster.Constants;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class AuthentificationView : Window
    {
        private readonly IAuthService _authentificationService;
        bool FinishedConnection;

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
                var url = e.Uri.Fragment;
                if (url.Contains("access_token") && url.Contains("#"))
                {
                    url = (new System.Text.RegularExpressions.Regex("#")).Replace(url, "?", 1);
                    PrivateInfo.Token = HttpUtility.ParseQueryString(url).Get("access_token");
                    FinishedConnection = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (FinishedConnection)
            {
                var viewModel = (HomeViewModel)DataContext;
                viewModel.CloseAuthentificationViewCommand.Execute(null);
                viewModel.WorkViewNavigationCommand.Execute(null);
            }
        }
    }
}
