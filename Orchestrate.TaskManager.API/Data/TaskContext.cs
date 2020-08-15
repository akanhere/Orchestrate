using System;
using Microsoft.EntityFrameworkCore;
using Orchestrate.TaskManager.API.Models;

namespace Orchestrate.TaskManager.API.Data
{
    public class TaskContext : DbContext
    {

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskDetail> TaskDetails { get; set; }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
    }
}
