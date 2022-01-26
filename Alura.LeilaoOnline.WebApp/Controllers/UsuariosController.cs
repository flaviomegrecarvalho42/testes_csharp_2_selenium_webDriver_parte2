using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IRepositorio<Usuario> _repositorio;

        public UsuariosController(IRepositorio<Usuario> repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost]
        public IActionResult Registrar(RegistroViewModel registroViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //registrar usuário/interessado
            var usuario = new Usuario
            {
                Email = registroViewModel.Email,
                Senha = registroViewModel.Password,
                Interessada = new Interessada(registroViewModel.Nome)
            };

            _repositorio.Incluir(usuario);
            return RedirectToAction("Agradecimento");
        }

        [HttpGet]
        public IActionResult Agradecimento()
        {
            return View();
        }
    }
}