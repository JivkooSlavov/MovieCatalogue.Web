using Microsoft.AspNetCore.Mvc;

namespace MovieCatalogue.Web.Areas.Admin.Controllers
{
    public class ReviewManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
