using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteServicos.Models;
using TesteServicos.Models.Generic;

namespace TesteServicos.DAL.Interfaces
{
    public interface IServicoRepository : IGenericRepository<Servico>
    {       

        IEnumerable<Servico> GetDataReport(DateTime dataInicial, DateTime dataFinal, int? cliente, string estado, string cidade, string bairro,
            int? tipoServico, decimal? valorMinimo, decimal? valorMaximo);

        IEnumerable<GastoClientePorMes> GastoClientesMes();

        IEnumerable<FornecedoresSemAtendimento> FornecedoresSemAtendimentos();

        IEnumerable<MediaValorCobradoServicoFornecedor> MediaValorCobradoServicoFornecedor();
    }
}