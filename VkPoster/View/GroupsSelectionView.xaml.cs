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
using VkPoster.ViewModel;

namespace VkPoster.View
{
    public partial class GroupsSelectionView : Page
    {
        public GroupsSelectionView()
        {
            InitializeComponent();
            var context = (GroupsSelectionViewModel)DataContext;
            var groupsModel = context.GroupsCollection;
            Groups.DataContext = groupsModel;
        }
    }
}
