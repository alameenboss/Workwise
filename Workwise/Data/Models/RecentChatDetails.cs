﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.Data.Models
{
    public class RecentChatDetails
    {
        public List<OnlineUserDetails> Users { get; set; }
        public int LastUserId { get; set; }
    }
}