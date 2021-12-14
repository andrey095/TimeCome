using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCome.Models
{
    public class Hashtag
    {
        public Hashtag()
        {
            MyTasks = new();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual List<MyTask> MyTasks { get; set; }
    }
}
