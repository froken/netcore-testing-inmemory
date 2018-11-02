using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Database.Models;

namespace Todo.BusinessLogic
{
    public interface ITaskManager
    {
        Task<List<TodoTask>> GetTasksAsync();

        Task<TodoTask> GetTaskAsync(string id);

        Task<TodoTask> CreateTaskAsync(TodoTask task);

        void DeleteTask(TodoTask task);

        void UpdateTask(TodoTask task);
    }
}
