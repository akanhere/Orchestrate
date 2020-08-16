using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrate.TaskManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Orchestrate.TaskManager.Web.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Orchestrate.TaskManager.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenIdConnect")]
    public class TaskController : Controller
    {

        IUserInfoService _userInfoService;
        ITaskService _taskService;
        string currentUser;

        public TaskController(IUserInfoService userInfoService, ITaskService taskService, IHttpContextAccessor context)
        {
            _userInfoService = userInfoService;
            _taskService = taskService;
            //var u = context.HttpContext.User.Claims.FirstOrDefault(c=>c.Type =="email").Value;

        }
        public async Task<IActionResult> Index()
        {
            var allTasks = await _taskService.GetTasksAsync();
            var tasks = new List<TaskViewModel>();
            foreach (var taskItem in allTasks)
            {
                tasks.Add(new TaskViewModel
                {
                    AssignedBy = taskItem.AssignedBy,
                    AssignedTo = taskItem.AssignedTo,
                    Category = taskItem.Category,
                    Description = taskItem.Description,
                    DueDate = taskItem.DueDate,
                    IsUrgent = taskItem.IsUrgent,
                    ServiceLocation = taskItem.ServiceLocation,
                    StatusId = Convert.ToInt32(taskItem.Status),
                    Title = taskItem.Title
                });
            }

            return View(tasks);
        }

        [HttpGet]
        [Authorize(Roles = "ServiceManager, PropertyOwner")]
        public async Task<IActionResult> Create()
        {
            var allUsers = await _userInfoService.GetAllUsers();
            var vm = new TaskViewModel(allUsers);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {

                //Convert to DTO and Post to Service
                _taskService.CreateTask(model);

            }
            return View(model);
        }
    }
}