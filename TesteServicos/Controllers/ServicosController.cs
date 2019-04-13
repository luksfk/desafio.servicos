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
    [Authorize]
    public class ServicosController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;

        public ServicosController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        // GET: Servicos
        public ActionResult Index()
        {
            var servicos = unitOfWork.ServicoRepository.Get(includeProperties: "Cliente");
            return View(Mapper.Map<List<Servico>, List<ServicoViewModel>>(servicos.ToList()));
        }

        // GET: Servicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Servico servico = unitOfWork.ServicoRepository.GetByID(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Servico, ServicoViewModel>(servico));
        }

        // GET: Servicos/Create
        public ActionResult Create()
        {
            PopulateDropDownCliente();
            PopulateDropDownTipoServico();
            return View();
        }

        // POST: Servicos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,DataAtendimento,ValorServico,ClienteId,TipoServicoId")] ServicoViewModel cadastroViewModel)
        {

            if (ModelState.IsValid)
            {
                var servico = Mapper.Map<ServicoViewModel, Servico>(cadastroViewModel);
                servico.FornecedorId = unitOfWork.ServicoRepository.FornecedorIdLogado;
                unitOfWork.ServicoRepository.Insert(servico);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            PopulateDropDownCliente(cadastroViewModel.ClienteId);
            PopulateDropDownTipoServico(cadastroViewModel.TipoServicoId);
            return View(cadastroViewModel);
        }

        // GET: Servicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Servico servico = unitOfWork.ServicoRepository.GetByID(id);
            if (servico == null)
            {
                return HttpNotFound();
            }

            PopulateDropDownCliente(servico.ClienteId);
            PopulateDropDownTipoServico(servico.TipoServicoId);
            return View(Mapper.Map<Servico, ServicoViewModel>(servico));
        }

        // POST: Servicos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,DataAtendimento,ValorServico,ClienteId,TipoServicoId")] ServicoViewModel editViewModel)
        {
            if (!RegistroExistente((int?)editViewModel.Id))
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                var servico = Mapper.Map<ServicoViewModel, Servico>(editViewModel);
                servico.FornecedorId = unitOfWork.ServicoRepository.FornecedorIdLogado;
                unitOfWork.ServicoRepository.Update(servico);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            PopulateDropDownCliente(editViewModel.ClienteId);
            PopulateDropDownTipoServico(editViewModel.TipoServicoId);
            return View(editViewModel);
        }

        // GET: Servicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = unitOfWork.ServicoRepository.GetByID(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Servico, ServicoViewModel>(servico));
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!RegistroExistente(id))
            {
                return HttpNotFound();
            }

            unitOfWork.ServicoRepository.Delete(id);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private void PopulateDropDownCliente(object selectedClient = null)
        {
            ViewBag.ClienteId = new SelectList(unitOfWork.ClienteRepository.Get(), "Id", "Nome", selectedClient);
        }

        private void PopulateDropDownTipoServico(object selectedClient = null)
        {
            ViewBag.TipoServicoId = new SelectList(unitOfWork.TipoServicoRepository.Get(), "Id", "Descricao", selectedClient);
        }

        private bool RegistroExistente(int? id)
        {
            return unitOfWork.ServicoRepository.Exists(id);
        }
    }
}
