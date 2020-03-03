using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AiDO.WebApp.Models;
using System.Threading.Tasks;

namespace AiDO.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersRepository _users;

        public HomeController(ILogger<HomeController> logger, IUsersRepository users)
        {
            _logger = logger;
            _users = users;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _users.GetUserNames();
            ViewData["Users"] = string.Join(", ", users);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
