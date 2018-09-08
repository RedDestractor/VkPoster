using GalaSoft.MvvmLight.Ioc;
using System;
using System.Web;
using System.Windows;
using VkNet.Model;
using VkPoster.Containers;
using VkPoster.Interfaces;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class AuthenticationView : Window
    {
        public AuthorizationResult Auth { get; set; }
        public string Tfa { get; set; }

        public AuthenticationView()
        {
            InitializeComponent();
        }
    }
}
