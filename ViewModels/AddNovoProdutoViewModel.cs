using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    internal class AddNovoProdutoViewModel : ObservableObject
    {
        private Produto _produto = new Produto();
        public Produto Produto
        {
            get => _produto;
            set => SetProperty(ref _produto, value);
        }

        public ObservableCollection<Categoria> Categorias { get; set; }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        public Action? FecharAction { get; set; }
        public Action? SucessoAction { get; set; }

        public AddNovoProdutoViewModel()
        {
            Categorias = new ObservableCollection<Categoria>(DataBaseManager.Instance.ObterTodasCategorias());

            SalvarCommand = new RelayCommand(SalvarProduto);
            CancelarCommand = new RelayCommand(() => FecharAction?.Invoke());
        }

        private void SalvarProduto()
        {
            if (!Validar())
            {
                MessageBox.Show("Preencha todos os campos obrigatórios!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Produto.Ativo = true;
            DataBaseManager.Instance.AdicionarProduto(Produto);

            MessageBox.Show("Produto adicionado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            SucessoAction?.Invoke(); // informa que deu certo (ex: define DialogResult = true)
            FecharAction?.Invoke();  // fecha a janela
        }

        private bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Produto.NomeProduto)
                && Produto.PrecoUnitario > 0
                && Produto.PrecoVenda > 0
                && !string.IsNullOrWhiteSpace(Produto.Categoria);
        }
    }
}
