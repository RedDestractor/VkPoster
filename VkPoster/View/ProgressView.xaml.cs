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
using System.Windows.Threading;
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class ProgressView : Page
    {
        public ProgressView()
        {
            InitializeComponent();

            var dispatcherTimer = new DispatcherTimer();
            var viewModel = (GroupsSelectionViewModel)DataContext;
            dispatcherTimer.Tick += new EventHandler(GetExecutionInTimerEvent);
            dispatcherTimer.Interval = new TimeSpan(0, viewModel.TimeForExecution, 0);
            dispatcherTimer.Start();
        }

        private void GetExecutionInTimerEvent(object sender, EventArgs e)
        {
            var viewModel = (GroupsSelectionViewModel)DataContext;
            viewModel.SetPostToAdminGroup.Execute(null);
        }
    }
}
