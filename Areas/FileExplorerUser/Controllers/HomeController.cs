using Microsoft.AspNetCore.Mvc;

namespace ProjectZero.Areas.FileExplorerUser.Controllers {
    [Area("FileExplorerUser")]
    [Route("KatalogPlikow")]
    public class HomeController : Controller{
        public HomeController (){
            
        }

        public IActionResult Index() {
            return View();
        }
    }
}