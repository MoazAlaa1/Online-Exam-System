using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.BL;
using OnlineExamSystem.Classes;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        IDashBoard _ClsDashboard;
        public HomeController(IDashBoard ClsDashboard)
        {
            _ClsDashboard = ClsDashboard;
        }
        public async Task<IActionResult> Index()
        {
            DashboardResult dashboard = await _ClsDashboard.GetDashboardDataAsync();
            return View(dashboard);
        }
    }
}
