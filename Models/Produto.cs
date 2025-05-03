using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.BancoDeDados
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public string Categoria { get; set; }
        public float PrecoUnitario { get; set; }
        public float PrecoVenda { get; set; }
        public float? PorcentagemDeLucro { get; set; }
        public long? CodigoBarra { get; set; }
        public bool Ativo {  get; set; }
    }
}
