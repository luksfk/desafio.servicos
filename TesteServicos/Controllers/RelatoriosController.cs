using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteServicos.DAL;
using TesteServicos.DAL.Interfaces;
using TesteServicos.Models;
using TesteServicos.ViewModels;

namespace TesteServicos.Controllers
{
    public class RelatoriosController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        public RelatoriosController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            PopulateDropDownCliente();
            PopulateDropDownTipoServico();
            return View();
        }

        [HttpPost]
        public PartialViewResult Relatorio(ConsultaRelatorioViewModel consulta)
        {
            var servicos = this.unitOfWork.ServicoRepository.GetDataReport(
                consulta.DataInicial, consulta.DataFinal, consulta.ClienteId, consulta.Estado, consulta.Cidade, consulta.Bairro, consulta.TipoServicoId,
                consulta.ValorMinimo, consulta.ValorMaximo);
            return PartialView(Mapper.Map<List<Servico>, List<ServicoViewModel>>(servicos.ToList()));
        }

        private void PopulateDropDownCliente()
        {
            ViewBag.ClienteId = new SelectList(unitOfWork.ClienteRepository.Get(), "Id", "Nome", null);
        }

        private void PopulateDropDownTipoServico()
        {
            ViewBag.TipoServicoId = new SelectList(unitOfWork.TipoServicoRepository.Get(), "Id", "Descricao", null);
        }
    }
}