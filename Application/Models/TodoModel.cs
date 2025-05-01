using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application.Models
{
    public class TodoModel
    {
        public string? Title { get; set; }
        public bool Status { get; set; }
    }

    public static class TodoModelDataMock
    {
        public static List<TodoModel> lstData = new List<TodoModel>()
        {
            new TodoModel{Title = "Task-01",Status = false },
            new TodoModel{Title = "Task-02",Status = false },
            new TodoModel{Title = "Task-03",Status = false },
        };
    }
}
