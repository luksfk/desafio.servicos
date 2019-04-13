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
    public class TipoServicosController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        public TipoServicosController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: TipoServicos
        public ActionResult Index()
        {
            var result = unitOfWork.TipoServicoRepository.Get().ToList();
            return View(Mapper.Map<List<TipoServico>, List<TipoServicoViewModel>>(result));
        }

        // GET: TipoServicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoServico tipoServico = unitOfWork.TipoServicoRepository.GetByID(id);
            if (tipoServico == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<TipoServico, TipoServicoViewModel>(tipoServico));
        }

        // GET: TipoServicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoServicos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao")] TipoServicoViewModel tipoServicoVm)
        {
            if (ModelState.IsValid)
            {
                var tipoServico = Mapper.Map<TipoServicoViewModel, TipoServico>(tipoServicoVm);
                tipoServico.FornecedorId = unitOfWork.TipoServicoRepository.FornecedorIdLogado;
                unitOfWork.TipoServicoRepository.Insert(tipoServico);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(tipoServicoVm);
        }

        // GET: TipoServicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoServico tipoServico = unitOfWork.TipoServicoRepository.GetByID(id);
            if (tipoServico == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<TipoServico, TipoServicoViewModel>(tipoServico));
        }

        // POST: TipoServicos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao")] TipoServicoViewModel tipoServicoVm)
        {
            if (!RegistroExistente(tipoServicoVm.Id))
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                var tipoServico = Mapper.Map<TipoServicoViewModel, TipoServico>(tipoServicoVm);
                tipoServico.FornecedorId = unitOfWork.TipoServicoRepository.FornecedorIdLogado;
                unitOfWork.TipoServicoRepository.Update(tipoServico);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(tipoServicoVm);
        }

        // GET: TipoServicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoServico tipoServico = unitOfWork.TipoServicoRepository.GetByID(id);
            if (tipoServico == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<TipoServico, TipoServicoViewModel>(tipoServico));
        }

        // POST: TipoServicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!RegistroExistente(id))
            {
                return HttpNotFound();
            }

            unitOfWork.TipoServicoRepository.Delete(id);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        private bool RegistroExistente(int? id)
        {
            return unitOfWork.TipoServicoRepository.Exists(id);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
