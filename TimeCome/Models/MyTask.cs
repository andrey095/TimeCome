using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCome.Models
{
    public class MyTask
    {
        public MyTask()
        {
            Hashtags = new();
            TaskFiles = new();
            MyTasks = new();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string About { get; set; }
        [MaxLength(200)]
        public string Comment { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual List<Hashtag> Hashtags { get; set; }
        public virtual List<TaskFile> TaskFiles { get; set; }


        public virtual List<MyTask> MyTasks { get; set; }
    }
}
