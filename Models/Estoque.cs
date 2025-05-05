using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public string Unidade { get; set; }
        public DateTime UltimaAlteracao { get; set; }
    }
}
