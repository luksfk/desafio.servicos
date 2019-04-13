using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteServicos.DAL;
using TesteServicos.DAL.Interfaces;

namespace TesteServicos.Controllers
{
    public class EstatisticasController : BaseController
    {
        private IUnitOfWork unitOfWork;
        public EstatisticasController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Estatisticas
        public ActionResult Index()
        {            
            return View();
        }

        public PartialViewResult FornecedoresSemAtendimento()
        {
            return PartialView(this.unitOfWork.ServicoRepository.FornecedoresSemAtendimentos());
        }

        public PartialViewResult GastoClientesMes()
        {
            return PartialView(this.unitOfWork.ServicoRepository.GastoClientesMes().ToList());
        }

        public PartialViewResult MediaValorCobradoServicoFornecedor()
        {
            return PartialView(this.unitOfWork.ServicoRepository.MediaValorCobradoServicoFornecedor().ToList());
        }

    }
}