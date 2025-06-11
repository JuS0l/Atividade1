using Microsoft.AspNetCore.Mvc;
using Atividade1.Repositorio;
using Atividade1.Models;

namespace Atividade1.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Produto()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.AdcionarProduto(produto);

                return RedirectToAction("Produto");
            }
            return View(produto);

        }
    }
}
