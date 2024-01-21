using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]

    public class ProdutosController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;

        

        public readonly InterfaceProductApp _InterfaceProductApp;

        public ProdutosController(InterfaceProductApp InterfaceProductApp, UserManager<ApplicationUser> userManager)
        {
            _InterfaceProductApp = InterfaceProductApp;
            _userManager = userManager;
    }

        // GET: ProdutosController1
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _InterfaceProductApp.ListarProdutosUsuario(idUsuario));
        }

        // GET: ProdutosController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        // GET: ProdutosController1/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {

                var idUsuario = await RetornarIdUsuarioLogado();

                produto.UserId = idUsuario;

                await _InterfaceProductApp.AddProduct(produto);
                if(produto.Notiycoes.Any())
                {
                    foreach (var item in produto.Notiycoes)
                    {
                        ModelState.AddModelError(item.NomePropiedade, item.mensagem);
                    }

                    return View("Create", produto);
                }

                

            }
            catch
            {
                return View("Create", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _InterfaceProductApp.UpdateProduct(produto);
                if (produto.Notiycoes.Any())
                {
                    foreach (var item in produto.Notiycoes)
                    {
                        ModelState.AddModelError(item.NomePropiedade, item.mensagem);
                    }

                    return View("Edit", produto);
                }



            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        
        }

        // GET: ProdutosController1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _InterfaceProductApp.GetEntityById(id);

                await _InterfaceProductApp.Delete(produto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        private async Task<string> RetornarIdUsuarioLogado()
        {
            var IdUsuario = await _userManager.GetUserAsync(User);

            return IdUsuario.Id;
        }
    }
}
