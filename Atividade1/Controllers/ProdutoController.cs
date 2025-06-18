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
        public IActionResult Index()
        {
            /* Retorna a View padrão associada a esta Action,
 passando como modelo a lista de todos os produtos obtido do repositório.*/
            return View(_produtoRepositorio.TodosProdutos());
        }
        public IActionResult Produto()
        {
            return View();
        }
        public IActionResult CadastroProd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroProd(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.AdcionarProduto(produto);

                return RedirectToAction("Produto", "Index");
            }
            return View(produto);

        }
    }
}
