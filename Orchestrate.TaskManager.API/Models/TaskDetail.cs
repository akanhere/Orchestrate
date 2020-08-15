using System;
namespace Orchestrate.TaskManager.API.Models
{
    public class TaskDetail
    {
        public int TaskDetailId { get; set; }
        public Task Task { get; set; }
        public string ImageUri { get; set; }
        public string Note { get; set; }

    }
}
