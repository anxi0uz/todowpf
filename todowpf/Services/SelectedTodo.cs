using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todowpf.Abstractions;
using todowpf.Models;

namespace todowpf.Services
{
    public class SelectedTodoService : ISelectedTodoService
    {
        public Todo Todo { get; set; }
    }
}
