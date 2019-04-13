using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesteServicos.Models;

namespace TesteServicos.ViewModels
{
    public class ServicoViewModel
    {
        [Key]
        public long Id { get; set; }        
        [Required]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [Required]
        [DisplayName("Data de Atendimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataAtendimento { get; set; }
        [Required]
        [DisplayName("Valor do Serviço")]
        public decimal ValorServico { get; set; }
        [Required]
        [DisplayName("Cliente")]
        public int? ClienteId { get; set; }
        [Required]
        [DisplayName("Tipo do Serviço")]
        public int? TipoServicoId { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public TipoServicoViewModel TipoServico { get; set; }
    }
}