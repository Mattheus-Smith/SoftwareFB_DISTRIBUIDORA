using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public int? NumeroMesa { get; set; }
        public DateTime DataAbertura { get; set; }
        public float ValorTotal { get; set; }
        public string Status { get; set; }
    }
}
