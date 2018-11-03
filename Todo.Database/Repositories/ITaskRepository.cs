using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Database.Models;

namespace Todo.Database.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TodoTask>> GetTasksAsync();

        Task<TodoTask> GetTaskAsync(int id);

        Task<TodoTask> CreateTaskAsync(TodoTask task);

        Task DeleteTaskAsync(int id);

        Task UpdateTaskAsync(int id, TodoTask task);
    }
}
