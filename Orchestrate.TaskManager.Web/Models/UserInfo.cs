﻿using System;
using System.Collections.Generic;

namespace Orchestrate.TaskManager.Web.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsActive { get; set; }

    }
}
