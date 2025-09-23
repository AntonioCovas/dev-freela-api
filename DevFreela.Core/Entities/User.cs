using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
            Active = true;

            UserSkills = new List<UserSkill>();
            FreelanceProjects = new List<Project>();
            OwnedProjects = new List<Project>();
            Comments = new List<ProjectComment>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<UserSkill> UserSkills { get; private set; }
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
        public List<ProjectComment> Comments { get; set; }
        public bool Active { get; set; }
        public UserTypeEnum Type { get; set; }

        public void Update(string fullName, string email, DateTime birthDate, bool active)
        {
            if(!string.IsNullOrEmpty(fullName)) FullName = fullName;
            if (!string.IsNullOrEmpty(email)) Email = email;
            if (birthDate != DateTime.MinValue) BirthDate = birthDate; 
            Active = active;
        }
    }
}
