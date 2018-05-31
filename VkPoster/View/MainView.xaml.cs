using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class MainView : Window
    { 
        public MainView()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage message)
        {
            if (message.Notification == "ConnetToVk")
            {
                var authentificationView = new AuthentificationView();
                authentificationView.Show();
            }
        }
    }
}