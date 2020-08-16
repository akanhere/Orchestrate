using System;
using System.Collections.Generic;

namespace Orchestrate.TaskManager.Web.Services.ModelDTO
{
    public class TaskDTO
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Category { get; set; }
        public string AssignedTo { get; set; }
        public ICollection<TaskDetailDTO> Details { get; set; }
        public string ServiceLocation { get; set; }
        public string GpsCoordinates { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime DueDate { get; set; }
        public string AssignedBy { get; set; }

    }
}
