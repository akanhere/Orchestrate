using System;
namespace Orchestrate.TaskManager.Web.Services.ModelDTO
{
    public class TaskDetailDTO
    {
        public TaskDetailDTO()
        {

        }

        public TaskDetailDTO(TaskDTO task)
        {
            Task = task;
        }

        public int TaskDetailId { get; set; }
        public TaskDTO Task { get; set; }
        public string ImageUri { get; set; }
        public string Note { get; set; }
    }
}
