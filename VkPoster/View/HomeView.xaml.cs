using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace VkPoster.View
{
    public partial class HomeView : Page
    {
        AuthenticationView _authenticationView;

        public HomeView()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage message)
        {
            if (message.Notification == "ConnectToVk")
            {
                _authenticationView = new AuthenticationView();

                _authenticationView.Show();
            }
            if (message.Notification == "ConnectionToVkFinished")
            {
                _authenticationView.Close();
            }
        }
    }
}
