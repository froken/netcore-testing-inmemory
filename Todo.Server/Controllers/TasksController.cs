using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Database.Models;
using Todo.Database.Repositories;
using Todo.Server.Models;

namespace Todo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoTaskModel>> Get()
        {
            var tasks = await _taskRepository.GetTasksAsync();
            return _mapper.Map<List<TodoTaskModel>>(tasks);
        }

        [HttpGet("{id}")]
        public async Task<TodoTaskModel> Get(string id)
        {
            var task = await _taskRepository.GetTaskAsync(id);
            return _mapper.Map<TodoTaskModel>(task);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]TodoTaskModel task)
        {
            var createdTask = await _taskRepository.CreateTaskAsync(_mapper.Map<TodoTask>(task));
            return CreatedAtAction("api/tasks", createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]TodoTaskModel task)
        {
            await _taskRepository.UpdateTaskAsync(id, _mapper.Map<TodoTask>(task));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _taskRepository.DeleteTaskAsync(id);
            return Ok();
        }
    }
}
