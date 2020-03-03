using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiDO.WebApp.Controllers
{
    public interface IUsersRepository
    {
        Task<IEnumerable<string>> GetUserNames();
    }
}
