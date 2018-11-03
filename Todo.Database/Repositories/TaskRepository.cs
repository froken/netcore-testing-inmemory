using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Database.Contexts;
using Todo.Database.Models;

namespace Todo.Database.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoContext _todoContext;

        public TaskRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public Task<List<TodoTask>> GetTasksAsync()
        {
            return _todoContext.Tasks.ToListAsync();
        }

        public Task<TodoTask> GetTaskAsync(int id)
        {
            return _todoContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TodoTask> CreateTaskAsync(TodoTask task)
        {
            var createdTask = await _todoContext.Tasks.AddAsync(task);
            await _todoContext.SaveChangesAsync();
            return createdTask.Entity;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var taskToDelete = await _todoContext.Tasks.FirstAsync(t => t.Id == id);
            _todoContext.Tasks.Remove(taskToDelete);
            await _todoContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(int id, TodoTask task)
        {
            var taskToUpdate = await _todoContext.Tasks.FirstAsync(t => t.Id == id);
            taskToUpdate.Text = task.Text;
            taskToUpdate.IsDone = task.IsDone;

            _todoContext.Tasks.Update(taskToUpdate);
            await _todoContext.SaveChangesAsync();
        }
    }
}
