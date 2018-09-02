using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using VkPoster.Interfaces;
using VkPoster.Services;
using VkPoster.Model;
using VkPoster.Service;
using VkNet;
using VkNet.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace VkPoster.ViewModel
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

            SimpleIoc.Default.Register<IVkApiService, VkApiService>();
            SimpleIoc.Default.Register<IAuthService, AuthentificationService>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("HomeView", new Uri("../View/HomeView.xaml", UriKind.Relative));
            navigationService.Configure("GroupsSelectionView", new Uri("../View/GroupsSelectionView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        public GroupsSelectionViewModel Groups
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GroupsSelectionViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}