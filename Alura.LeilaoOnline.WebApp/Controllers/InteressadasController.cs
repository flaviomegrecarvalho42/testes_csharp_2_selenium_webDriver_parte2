using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Filtros;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.Core;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Extensions;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [AutorizacaoFilter]
    public class InteressadasController : Controller
    {
        private readonly IRepositorio<Leilao> _repositorioLeilao;
        private readonly IRepositorio<Interessada> _repositorioInteressada;

        public InteressadasController(IRepositorio<Leilao> repoLeilao, IRepositorio<Interessada> repoInteressada)
        {
            _repositorioLeilao = repoLeilao;
            _repositorioInteressada = repoInteressada;
        }

        public IActionResult Index(PesquisaLeiloesViewModel pesquisa)
        {
            var usuarioLogado = this.HttpContext.Session.Get<Usuario>("usuarioLogado");
            var interessada = _repositorioInteressada.BuscarPorId(usuarioLogado.Interessada.Id);

            var model = new DashboardInteressadaViewModel
            {
                MeusLances = interessada.Lances,
                LeiloesFavoritos = interessada.Favoritos.Select(f => f.LeilaoFavorito),
                LeiloesPesquisados = PesquisaLeiloes(pesquisa)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult OfertarLance(LanceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Leilao leilao = _repositorioLeilao.BuscarPorId(model.LeilaoId);
            Interessada interessada = _repositorioInteressada.BuscarPorId(model.UsuarioLogadoId);

            leilao.ReceberLance(interessada, model.Valor);
            _repositorioLeilao.Alterar(leilao);

            return Ok();
        }

        [HttpPost]
        public IActionResult SeguirLeilao(FavoritoViewModel model)
        {
            var leilao = _repositorioLeilao.BuscarPorId(model.IdLeilao);

            if (leilao == null)
            {
                return NotFound();
            }

            var favorito = new Favorito
            {
                IdLeilao = model.IdLeilao,
                IdInteressada = model.IdInteressada
            };

            leilao.Seguidores.Add(favorito);
            _repositorioLeilao.Alterar(leilao);

            return Ok();
        }

        [HttpPost]
        public IActionResult AbandonarLeilao(FavoritoViewModel model)
        {
            var leilao = _repositorioLeilao.BuscarPorId(model.IdLeilao);

            if (leilao == null)
            {
                return NotFound();
            }

            var favorito = leilao.Seguidores
                                 .FirstOrDefault(s => s.IdLeilao == model.IdLeilao &&
                                                      s.IdInteressada == model.IdInteressada);

            leilao.Seguidores.Remove(favorito);
            _repositorioLeilao.Alterar(leilao);

            return Ok();
        }

        private IEnumerable<Leilao> PesquisaLeiloes(PesquisaLeiloesViewModel pesquisa)
        {
            if (pesquisa.Andamento == null && pesquisa.Categorias == null && pesquisa.Termo == null)
            {
                return null;
            }

            var leiloes = _repositorioLeilao.Todos;

            if (!string.IsNullOrEmpty(pesquisa.Andamento))
            {
                leiloes = leiloes.Where(l => l.Estado == EstadoLeilao.LeilaoEmAndamento);
            }

            if (!string.IsNullOrWhiteSpace(pesquisa.Termo))
            {
                leiloes = leiloes.Where(l => l.Titulo.Contains(pesquisa.Termo, StringComparison.InvariantCultureIgnoreCase));
            }

            if (pesquisa.Categorias != null && pesquisa.Categorias.Length > 0)
            {
                leiloes = leiloes.Where(l => pesquisa.Categorias.Contains(l.Categoria));
            }

            return leiloes.ToList();
        }
    }
}