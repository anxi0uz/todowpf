using todowpf.Models;

namespace todowpf.Abstractions
{
    public interface ISelectedTodoService
    {
        Todo Todo { get; set; }
    }
}