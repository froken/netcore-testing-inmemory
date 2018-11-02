using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Database.Contexts;
using Todo.Database.Models;

namespace Todo.BusinessLogic
{
    public class TaskManager : ITaskManager
    {
        private readonly TodoContext _todoContext;

        public TaskManager(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public Task<List<TodoTask>> GetTasksAsync()
        {
            return _todoContext.Tasks.ToListAsync();
        }

        public Task<TodoTask> GetTaskAsync(string id)
        {
            return _todoContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TodoTask> CreateTaskAsync(TodoTask task)
        {
            var createdTask = await _todoContext.Tasks.AddAsync(task);
            _todoContext.SaveChanges();

            return createdTask.Entity;
        }

        public void DeleteTask(TodoTask task)
        {
            _todoContext.Tasks.Remove(task);
        }

        public void UpdateTask(TodoTask task)
        {
            _todoContext.Tasks.Update(task);
        }
    }
}
