using Microsoft.AspNetCore.Mvc;
using JobLocal.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobLocal.Controllers
{
    public class AccountController : Controller
    {
        private static List<Usuario> _usuarios = new List<Usuario>();
        private static int _nextUserId = 1;

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
            if (usuario != null)
            {
                // Simples sistema de sessão
                HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "E-mail ou senha incorretos";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (_usuarios.Any(u => u.Email == usuario.Email))
            {
                ViewBag.Error = "E-mail já cadastrado";
                return View();
            }

            usuario.Id = _nextUserId++;
            _usuarios.Add(usuario);

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Perfil()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            // ⭐⭐ CORREÇÃO: Redirecionar para o Perfil no JobsController ⭐⭐
            return RedirectToAction("Perfil", "Jobs");
        }
    }
}