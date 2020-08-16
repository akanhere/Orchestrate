using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orchestrate.TaskManager.Web.Models;
using Orchestrate.TaskManager.Web.Services.ModelDTO;

namespace Orchestrate.TaskManager.Web.Services
{
    public interface ITaskService
    {

        // Task
        Task CreateTask(TaskViewModel task);
        Task<IEnumerable<TaskDTO>> GetTasksAsync();
    }
}
