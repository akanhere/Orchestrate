using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orchestrate.TaskManager.Web.Annotations;

namespace Orchestrate.TaskManager.Web.Models
{
    public class TaskViewModel
    {
        public TaskViewModel()
        {
        }
        [Required]
        public string Title { get; set; }


        public string Description { get; set; }

        [Display(Name = "Status")]
        public List<SelectListItem> StatusList => GetAllStatus();

        public int StatusId { get; set; }

        [Required]
        public string Category { get; set; }

        public string AssignedTo { get; set; }

        public List<string> ImageUri { get; set; }

        public List<string> Notes { get; set; }

        [Required]
        [Display(Name = "Service Location / Address")]
        public string ServiceLocation { get; set; }

        [LatitudeCoordinate]
        public double Lat { get; set; }

        [LongitudeCoordinate]
        public double Lon { get; set; }

        public bool IsUrgent { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        public string AssignedBy { get; set; }



        private List<SelectListItem> GetAllStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Open", "1"),
                new SelectListItem("Assigned","2"),
                new SelectListItem("In Progress","3"),
                new SelectListItem("Waiting for Customer", "4"),
                new SelectListItem("Deffered","5"),
                new SelectListItem("Closed","6"),
                new SelectListItem("Cancelled","7")
            };
        }
    }
}
