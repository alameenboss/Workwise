using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.Model
{
    public class UpdateMessageStatusRequest
    {
        public string CurrentUserId { get; set; }
        public string FromUserId { get; set; }
    }
}
