using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrate.TaskManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Orchestrate.TaskManager.Web.Services;

namespace Orchestrate.TaskManager.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenIdConnect")]
    public class TaskController : Controller
    {
        
        IUserInfoService _userInfoService;

        public TaskController(IUserInfoService userInfoService)
        {
           _userInfoService = userInfoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="ServiceManager")]
        public async Task<IActionResult> Create()
        {
            var allUsers = await _userInfoService.GetAllUsers();
            var vm = new TaskViewModel(allUsers);
            return View(vm);
        }

        public IActionResult Create(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}