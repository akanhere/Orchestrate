using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orchestrate.TaskManager.Web.Models;

namespace Orchestrate.TaskManager.Web.Services
{
    public interface IUserInfoService
    {
        Task<List<UserInfo>> GetAllUsers();
    }
}
