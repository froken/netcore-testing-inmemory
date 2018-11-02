using AutoMapper;
using Todo.Database.Models;
using Todo.Server.Models;

namespace Todo.Server
{
    public class TodoMapperProfile : Profile
    {
        public TodoMapperProfile()
        {
            CreateMap<TodoTask, TodoTaskModel>().ReverseMap();
        }
    }
}
