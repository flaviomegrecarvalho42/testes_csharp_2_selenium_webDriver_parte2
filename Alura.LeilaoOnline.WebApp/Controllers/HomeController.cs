using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.Core;
using System.Linq;
using Alura.LeilaoOnline.WebApp.Extensions;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorio<Leilao> _repositorioLeilao;
        private readonly IRepositorio<Interessada> _repositorioInteressada;

        public HomeController(IRepositorio<Leilao> repositorio, IRepositorio<Interessada> repoInt)
        {
            _repositorioLeilao = repositorio;
            _repositorioInteressada = repoInt;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var proximosLeiloes = _repositorioLeilao.Todos
                                                    .Where(l => l.Estado == EstadoLeilao.LeilaoAntesDoPregao)
                                                    .OrderBy(l => l.InicioPregao)
                                                    .Take(6)
                                                    .Select(l => l.ToLeilaoViewModel())
                                                    .ToList();

            var usuarioLogado = HttpContext.Session.Get<Usuario>("usuarioLogado");

            if (Usuario.EhInteressada(usuarioLogado))
            {
                var interessada = _repositorioInteressada.BuscarPorId(usuarioLogado.Interessada.Id);

                proximosLeiloes.ForEach(l => l.SendoSeguido = interessada.Favoritos
                                                                         .Select(f => f.IdLeilao)
                                                                         .Any(id => id == l.Id));
            }

            return View(proximosLeiloes);
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var leilao = _repositorioLeilao.BuscarPorId(id).ToLeilaoViewModel();

            if (leilao == null)
            {
                return NotFound();
            }

            var usuarioLogado = HttpContext.Session.Get<Usuario>("usuarioLogado");

            if (Usuario.EhInteressada(usuarioLogado))
            {
                var interessada = _repositorioInteressada.BuscarPorId(usuarioLogado.Interessada.Id);

                leilao.SendoSeguido = interessada.Favoritos
                                                 .Select(f => f.IdLeilao)
                                                 .Any(idLeilao => idLeilao == leilao.Id);
            }

            return View(leilao);
        }

        [HttpGet]
        public IActionResult Categoria(string id)
        {
            ViewData["categoria"] = id;

            var leiloes = _repositorioLeilao.Todos
                                            .Where(l => l.Categoria == id)
                                            .Select(l => l.ToLeilaoViewModel());

            return View("Index", leiloes);
        }
    }
}