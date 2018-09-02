using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace VkPoster.View
{
    public partial class HomeView : Page
    {
        AuthentificationView authentificationView;

        public HomeView()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage message)
        {
            if (message.Notification == "ConnetToVk")
            {
                authentificationView = new AuthentificationView();

                authentificationView.Show();
            }
            if (message.Notification == "ConnectionToVkFinished")
            {
                authentificationView.Close();
            }
        }
    }
}
