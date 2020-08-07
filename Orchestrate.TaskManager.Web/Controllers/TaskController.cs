using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrate.TaskManager.Web.Models;

namespace Orchestrate.TaskManager.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenIdConnect")]
    public class TaskController : Controller
    {
        public TaskController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="ServiceManager")]
        public IActionResult Create()
        {
            var vm = new TaskViewModel();
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