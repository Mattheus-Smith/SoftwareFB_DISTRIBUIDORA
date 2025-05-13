using CommunityToolkit.Mvvm.Input;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using SoftwareFB_DISTRIBUIDORA.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Produto> Produtos { get; set; }

        public ICommand AbrirAddProdutoCommand { get; }
        public ICommand EditarProdutoCommand { get; }

        public ProdutoViewModel()
        {
            AbrirAddProdutoCommand = new RelayCommand(AbrirAddProduto);
            EditarProdutoCommand = new RelayCommand<object>(produto => EditarProduto(produto as Produto));

            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = DataBaseManager.Instance.ObterTodosProdutos();
            Produtos = new ObservableCollection<Produto>(produtos);
            OnPropertyChanged(nameof(Produtos));
        }

        public void AtualizarListaProdutos()
        {
            var produtos = DataBaseManager.Instance.ObterTodosProdutos();
            Produtos = new ObservableCollection<Produto>(produtos);
            OnPropertyChanged(nameof(Produtos));
        }

        private void AbrirAddProduto()
        {
            var novaJanela = new AddNovoProdutoView();
            bool? resultado = novaJanela.ShowDialog();

            if (resultado == true)
            {
                AtualizarListaProdutos();
            }
        }

        private void EditarProduto(Produto produtoSelecionado)
        {
            if (produtoSelecionado == null)
                return;

            var novaJanela = new EditProdutoView(produtoSelecionado);
            bool? resultado = novaJanela.ShowDialog();

            if (resultado == true)
            {
                AtualizarListaProdutos();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
