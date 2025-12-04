using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todowpf.Services
{
    public class InMemoryTokenStorage : ITokenStorage
    {
        public string Token { get; set; }

        public int UserId { get; set; }
        public void Clear()
        {
            Token = string.Empty;
            UserId = 0;
        }
    }
}
