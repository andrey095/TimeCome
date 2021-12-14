using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCome.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            MyTasks = new();
        }
        public virtual List<MyTask> MyTasks { get; set; }
    }
}
