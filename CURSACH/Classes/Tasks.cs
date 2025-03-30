using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CURSACH
{
    public class Tasks
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public int? AssignedUserId { get; set; }  // Исполнитель (может быть null)
        public int ReporterId { get; set; }  // Создатель задачи
        public int TypeId { get; set; }  // Тип задачи
        public int TaskStatusId { get; set; }  // Статус задачи
        public int TaskPriorityId { get; set; }  // Приоритет задачи
        public string Comments { get; set; }
        public decimal TimeAllocated { get; set; }  // Выделенное время
        public decimal? TimeSpent { get; set; }  // Потраченное время (может быть null)

        
    }

    public class TaskStatus
    {
        public int StatusId { get; set; }
        public string Title { get; set; }
    }

    public class TaskPriority
    {
        public int PriorityId { get; set; }
        public string Title { get; set; }
    }

    public class TaskType
    {
        public int TypeId { get; set; }
        public string Title { get; set; }
    }
}
