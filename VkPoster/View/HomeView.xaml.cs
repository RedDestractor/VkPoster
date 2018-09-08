using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using VkNet.Wpf;
using VkPoster.Containers;
using VkPoster.Interfaces;

namespace VkPoster.View
{
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}
