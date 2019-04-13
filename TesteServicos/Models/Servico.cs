using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteServicos.Models
{
    public class Servico : Entity
    {        
        public string Descricao { get; set; }
        public DateTime DataAtendimento { get; set; }
        public decimal ValorServico { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }        
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual TipoServico TipoServico { get; set; }
        public int TipoServicoId { get; set; }
    }
}