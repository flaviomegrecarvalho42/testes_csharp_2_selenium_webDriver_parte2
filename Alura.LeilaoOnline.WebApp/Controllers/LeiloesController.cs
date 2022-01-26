using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.Core;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados;
using System.Linq;
using Alura.LeilaoOnline.WebApp.Extensions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Alura.LeilaoOnline.WebApp.Filtros;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [AutorizacaoFilterAttribute]
    public class LeiloesController : Controller
    {
        private readonly IRepositorio<Leilao> _repositorio;
        private readonly IHostingEnvironment _hostingEnviroment;

        public LeiloesController(IRepositorio<Leilao> repositorio, IHostingEnvironment environment)
        {
            _repositorio = repositorio;
            _hostingEnviroment = environment;
        }

        public IActionResult Index(Paginacao paginacao)
        {
            var leiloes = _repositorio.Todos
                                      .Select(l => l.ToLeilaoViewModel())
                                      .ToListaPaginada(paginacao);

            return View(leiloes);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Novo(LeilaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Novo", model);
            }

            //gravar arquivo com a imagem definida
            model.Imagem = this.TentarGravarImagemDestaqueERetornarSeuNome(model.ArquivoImagem);
            var novoLeilao = model.ToLeilaoModel();

            _repositorio.Incluir(novoLeilao);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            //var leilao = _repo.BuscarPorId(id);
            var leilao = new LeilaoViewModel();

            if(leilao == null)
            {
                return NotFound();
            }

            //_repo.Excluir(leilao);
            return RedirectToAction("Index");
        }

        private string TentarGravarImagemDestaqueERetornarSeuNome(IFormFile upload)
        {
            if (upload != null)
            {
                var nomeArquivoServidor = Path.Combine(_hostingEnviroment.WebRootPath,
                                                       "images",
                                                       upload.FileName);

                using (var stream = new FileStream(nomeArquivoServidor, FileMode.OpenOrCreate))
                {
                    upload.CopyTo(stream);
                }
            }

            return $"/images/{upload.FileName}";
        }
    }
}