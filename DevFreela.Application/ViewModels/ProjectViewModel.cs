using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, DateTime createdAt, DateTime startedAt, ProjectStatusEnum status)
        {
            Id = id;
            Title = title;
            CreatedAt = createdAt;
            Status = status;
            StartedAt = startedAt;
        }

        public int Id { get; set; }
        public string Title { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime StartedAt { get; private set; }
    }
}
