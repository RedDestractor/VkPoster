using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
