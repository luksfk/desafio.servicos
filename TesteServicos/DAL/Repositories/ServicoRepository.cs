using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TesteServicos.DAL.Interfaces;
using TesteServicos.Models;
using TesteServicos.Models.Generic;
using TesteServicos.Utils;

namespace TesteServicos.DAL.Repositories
{
    public class ServicoRepository : GenericRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(ApplicationDbContext context, IUserService userService) : base(context, userService)
        {
        }       


        public IEnumerable<Servico> GetDataReport(DateTime dataInicial, DateTime dataFinal, int? cliente, string estado, string cidade, string bairro,
            int? tipoServico, decimal? valorMinimo, decimal? valorMaximo)
        {
            IQueryable<Servico> query = dbSet;
            query = query.Where(t => t.DataAtendimento >= dataInicial && t.DataAtendimento <= dataFinal);

            if (cliente.HasValue) query = query.Where(t => t.ClienteId == cliente);
            if (!string.IsNullOrEmpty(estado)) query = query.Where(t => t.Cliente.Estado.ToUpper().Contains(estado.ToUpper()));
            if (!string.IsNullOrEmpty(cidade)) query = query.Where(t => t.Cliente.Cidade.ToUpper().Contains(cidade.ToUpper()));
            if (!string.IsNullOrEmpty(bairro)) query = query.Where(t => t.Cliente.Bairro.ToUpper().Contains(bairro.ToUpper()));
            if (tipoServico.HasValue) query = query.Where(t => t.TipoServicoId == tipoServico);
            if (valorMinimo.HasValue) query = query.Where(t => t.ValorServico >= valorMinimo);
            if (valorMaximo.HasValue) query = query.Where(t => t.ValorServico <= valorMaximo);

            return query;
        }

        public IEnumerable<GastoClientePorMes> GastoClientesMes()
        {
            var result = context.Database.SqlQuery<GastoClientePorMes>(@"select * from GastoClientesMes()").ToList();

            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            for (int i = startDate.Month; i <= DateTime.Now.Month; i++)
            {
                if (!result.Any(t => t.Mes == i))
                {
                    result.Add(new GastoClientePorMes { Cliente = "SEM CLIENTES", Mes = i });
                }
            }

            return result;
        }

        public IEnumerable<FornecedoresSemAtendimento> FornecedoresSemAtendimentos()
        {
            var result = context.Database.SqlQuery<FornecedoresSemAtendimento>(@"select * from FornecedoresSemAtendimento()").ToList();

            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            for (int i = startDate.Month; i <= DateTime.Now.Month; i++)
            {
                if (!result.Any(t => t.Mes == i))
                {
                    result.Add(new FornecedoresSemAtendimento { Fornecedor = "NENHUM FORNECEDOR SEM SERVIÇO", Mes = i });
                }
            }

            return result;
        }

        public IEnumerable<MediaValorCobradoServicoFornecedor> MediaValorCobradoServicoFornecedor()
        {
            return context.Database.SqlQuery<MediaValorCobradoServicoFornecedor>(@"select * from MediaValorCobradoServicoFornecedor()").ToList();
        }
    }
}