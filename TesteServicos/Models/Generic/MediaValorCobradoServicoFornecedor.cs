using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteServicos.Models.Generic
{
    public class MediaValorCobradoServicoFornecedor
    {
        public string Fornecedor { get; set; }
        public string Descricao { get; set; }
        public decimal? Media { get; set; }
    }
}