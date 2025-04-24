using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models
{
    internal class ComandaItem
    {
        public int Id { get; set; }
        public int IdComanda { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public float PrecoTotal { get; set; }
        public DateTime HoraVenda { get; set; }
    }
}
