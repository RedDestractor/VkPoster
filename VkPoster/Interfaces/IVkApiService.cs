using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Utils;
using VkPoster.Model;

namespace VkPoster.Interfaces
{
    public interface IVkApiService
    {
        List<GroupDto> GetGroups(bool IsAdminOnly);
    }
}
