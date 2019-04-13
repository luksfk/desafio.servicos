using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteServicos.Models.Generic
{
    public class GastoClientePorMes
    {
        public int Mes { get; set; }
        public decimal TotalServicos { get; set; }
        public string Cliente { get; set; }
        public string NomeMes
        {
            get
            {
                return new DateTime(DateTime.Now.Year, Mes, 1).ToString("MMMM");
            }
        }
    }
}