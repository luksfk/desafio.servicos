using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteServicos.ViewModels
{
    public class ConsultaRelatorioViewModel
    {
        [Required]
        public DateTime DataInicial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
        public int? ClienteId { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public int? TipoServicoId { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
    }
}