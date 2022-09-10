using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GithubSocial : Entity
    {
        public string GithubUrl { get; set; }
        public virtual User? User { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public GithubSocial()
        {

        }

        public GithubSocial(string githubUrl, int userId)
        {
            UserId = userId;
            GithubUrl = githubUrl;
        }
    }
}
