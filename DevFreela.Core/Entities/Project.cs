using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project(string title, string description, int clientId, int freelancerId, decimal totalCost)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            TotalCost = totalCost;
            CreatedAt = DateTime.Now;

            Comments = new List<ProjectComment>();
            Status = ProjectStatusEnum.Created;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ClientId { get; private set; }
        public User Client { get; private set; }
        public int FreelancerId { get; private set; }
        public User Freelancer { get; private set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }

        public void Cancel()
        {
            if (Status == ProjectStatusEnum.InProgress)
                Status = ProjectStatusEnum.Canceled;
        }

        public void Finish()
        {
            if (Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Finished;
                FinishedAt = DateTime.Now;
            }
        }

        public void Start()
        {
            if (Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.InProgress;
                StartedAt = DateTime.Now;   
            }
        }

        public void Update(string title, string description, decimal totalCost)
        {
            if (!string.IsNullOrEmpty(title)) Title = title;
            if(!string.IsNullOrEmpty(description)) Description = description;  
            if(totalCost > 0m) TotalCost = totalCost;  
        }
    }
}
