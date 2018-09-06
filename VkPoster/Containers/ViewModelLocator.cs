using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using VkPoster.Interfaces;
using VkPoster.Model;
using VkPoster.Services;
using VkPoster.ViewModel;

namespace VkPoster.Containers
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<GroupsSelectionViewModel>();

            SetupNavigation();

            SimpleIoc.Default.Register<IAuthService, AuthenticationService>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("HomeView", new Uri("../View/HomeView.xaml", UriKind.Relative));
            navigationService.Configure("GroupsSelectionView", new Uri("../View/GroupsSelectionView.xaml", UriKind.Relative));
            navigationService.Configure("AdminGroupsSelectionView", new Uri("../View/AdminGroupsSelectionView.xaml", UriKind.Relative));
            navigationService.Configure("SetTimeView", new Uri("../View/SetTimeView.xaml", UriKind.Relative));
            navigationService.Configure("ProgressView", new Uri("../View/ProgressView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();

        public GroupsSelectionViewModel Groups => ServiceLocator.Current.GetInstance<GroupsSelectionViewModel>();

        public static void Cleanup()
        {
        }
    }
}