using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteServicos.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}