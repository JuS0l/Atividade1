using Microsoft.AspNetCore.Mvc;

namespace Atividade1.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
