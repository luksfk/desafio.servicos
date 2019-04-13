using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TesteServicos.ViewModels
{
    public class TipoServicoViewModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tipo do Serviço")]
        [Required]
        public string Descricao { get; set; }

    }
}