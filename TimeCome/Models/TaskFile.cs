using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCome.Models
{
    public class TaskFile
    {
        public TaskFile()
        {
            MyTasks = new();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual List<MyTask> MyTasks { get; set; }
    }
}
