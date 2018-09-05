using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VkNet.Model;

namespace VkPoster.Model
{
    public class GroupDto
    {
        public long Id { get; set; }
        public BitmapImage Image { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAdmin { get; set; }
    }
}
