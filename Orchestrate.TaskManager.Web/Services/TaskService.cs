using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Orchestrate.TaskManager.Web.Models;
using Orchestrate.TaskManager.Web.Services.ModelDTO;

namespace Orchestrate.TaskManager.Web.Services
{
    public class TaskService : ITaskService
    {
        private HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IOptions<AppSettings> _settings;

        public TaskService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _remoteServiceBaseUrl = $"{settings.Value.TaskApiUri}/api/task";
        }

        public async Task CreateTask(TaskViewModel task)
        {
            if (task != null)
            {
                var taskDto = new TaskDTO
                {
                    Title = task.Title,
                    Description = task.Description,
                    AssignedBy = task.AssignedBy,
                    AssignedTo = task.AssignedTo,
                    Category = task.Category,
                    Created = DateTime.UtcNow,
                    DueDate = task.DueDate,
                    IsUrgent = task.IsUrgent,
                    ServiceLocation = task.ServiceLocation,
                    LastUpdated = DateTime.UtcNow,
                    Status = task.StatusId.ToString()
                };

                var uri = _remoteServiceBaseUrl;
                var content = new StringContent(JsonConvert.SerializeObject(taskDto), System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

            }
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
        {
            var uri = _remoteServiceBaseUrl;
            List<TaskDTO> tasks = null;

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var allTasks = JsonConvert.DeserializeObject<IEnumerable<TaskDTO>>(responseString);

                if (allTasks != null)
                {
                    tasks = new List<TaskDTO>(allTasks);
                    //foreach (var taskItem in allTasks)
                    //{
                    //    tasks.Add(new TaskViewModel
                    //    {
                    //        AssignedBy = taskItem.AssignedBy,
                    //        AssignedTo = taskItem.AssignedTo,
                    //        Category = taskItem.Category,
                    //        Description = taskItem.Description,
                    //        DueDate = taskItem.DueDate,
                    //        IsUrgent = taskItem.IsUrgent,
                    //        ServiceLocation = taskItem.ServiceLocation,
                    //        StatusId = Convert.ToInt32(taskItem.Status),
                    //        Title = taskItem.Title
                    //    });
                    //}
                }
            }
            return tasks;
        }

    }
}
