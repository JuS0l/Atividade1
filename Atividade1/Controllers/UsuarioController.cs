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

        public IActionResult CadastroUsu()
        {
            return View();
        }
  
        [HttpPost]
        public IActionResult CadastroUsu(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                _usuarioRepositorio.AdcionarUsuario(usuario);

                return RedirectToAction("Login");

            }
            return View(usuario);
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {

            var usuario = _usuarioRepositorio.ObterUsuario(email);
            if (usuario != null && usuario.Senha == senha) {

                return RedirectToAction("Index", "Produto");

            }
            ModelState.AddModelError("", "Email ou Senha invalidos");
            return View();

        }
    }
}
