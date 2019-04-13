using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteServicos.DAL;
using TesteServicos.DAL.Interfaces;
using TesteServicos.Models;
using TesteServicos.ViewModels;

namespace TesteServicos.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientesController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        public ClientesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Clientes
        public ActionResult Index()
        {
            var itens = unitOfWork.ClienteRepository.Get();
            return View(Mapper.Map<List<Cliente>, List<ClienteViewModel>>(itens.ToList()));
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = unitOfWork.ClienteRepository.GetByID(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Cliente, ClienteViewModel>(cliente));
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Bairro,Cidade,Estado")] ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);
                cliente.FornecedorId = unitOfWork.ClienteRepository.FornecedorIdLogado;
                unitOfWork.ClienteRepository.Insert(cliente);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(clienteViewModel);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = unitOfWork.ClienteRepository.GetByID(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Cliente, ClienteViewModel>(cliente));
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Bairro,Cidade,Estado")] ClienteViewModel clienteViewModel)
        {
            if (!RegistroExistente(clienteViewModel.Id))
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                var cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteViewModel);
                cliente.FornecedorId = unitOfWork.ClienteRepository.FornecedorIdLogado;
                unitOfWork.ClienteRepository.Update(cliente);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(clienteViewModel);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cliente = unitOfWork.ClienteRepository.GetByID(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Cliente, ClienteViewModel>(cliente));
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!RegistroExistente(id))
            {
                return HttpNotFound();
            }

            unitOfWork.ClienteRepository.Delete(id);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
        private bool RegistroExistente(int? id)
        {
            return unitOfWork.ClienteRepository.Exists(id);
        }
    }
}

