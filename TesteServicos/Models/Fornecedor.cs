using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteServicos.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Fornecedor(string nome)
        {
            Nome = nome;
        }
    }
}