using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiDO.WebApp.Controllers
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly IEnumerable<string> _names = new[] {"Alice", "Bob"};

        public Task<IEnumerable<string>> GetUserNames() => Task.FromResult(_names);
    }
}
