using Microsoft.AspNetCore.Identity;
using Atividade1.Models;
using Atividade1.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Atividade1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.AdcionarUsuario(usuario);

                return RedirectToAction("Usuario")
            }

        }
    }
}
