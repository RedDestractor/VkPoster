using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VkPoster.Constants;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class AuthentificationView : Window
    {
        private readonly IAuthService _authentificationService;

        public AuthentificationView()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();

            _authentificationService = SimpleIoc.Default.GetInstance<IAuthService>();
            _authentificationService.GetOauthPage(webBrowser);
        }

        private void webBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                var url = e.Uri.Fragment;
                if (url.Contains("access_token") && url.Contains("#"))
                {
                    url = (new System.Text.RegularExpressions.Regex("#")).Replace(url, "?", 1);
                    PrivateInfo.Token = HttpUtility.ParseQueryString(url).Get("access_token");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteVkCookie()
        {
            var cookie = String.Format("c_user=; expires={0:R}; path=/; domain=.facebook.com", DateTime.UtcNow.AddDays(-1).ToString("R"));
            Application.SetCookie(new Uri("https://www.vk.com"), cookie);
        }
    }
}
