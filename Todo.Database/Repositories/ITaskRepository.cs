using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Database.Models;

namespace Todo.Database.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TodoTask>> GetTasksAsync();

        Task<TodoTask> GetTaskAsync(string id);

        Task<TodoTask> CreateTaskAsync(TodoTask task);

        Task DeleteTaskAsync(string id);

        Task UpdateTaskAsync(string id, TodoTask task);
    }
}
