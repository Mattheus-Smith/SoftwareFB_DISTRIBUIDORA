using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    internal class ProdutoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Produto> Produtos { get; set; } //Notifica automaticamente a interface gráfica (UI) quando itens são adicionados, removidos ou a coleção é atualizada.

        public ProdutoViewModel()
        {
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = DataBaseManager.Instance.ObterTodosProdutos();
            Produtos = new ObservableCollection<Produto>(produtos); //Estou atualizando a coleção de produtos
            OnPropertyChanged(nameof(Produtos)); //Avise a interface (UI) que a propriedade mudou.
        }

        public void AtualizarListaProdutos()
        {
            var produtos = DataBaseManager.Instance.ObterTodosProdutos();
            Produtos = new ObservableCollection<Produto>(produtos);
            OnPropertyChanged(nameof(Produtos));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) //Esse método avisa a UI: "Ei, a propriedade Produtos mudou!".
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
