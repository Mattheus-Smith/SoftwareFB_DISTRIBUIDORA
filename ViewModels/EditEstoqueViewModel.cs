using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    public class EditEstoqueViewModel : ObservableObject
    {
        private Estoque estoqueSelecionado;
        public Estoque EstoqueSelecionado
        {
            get => estoqueSelecionado;
            set => SetProperty(ref estoqueSelecionado, value);
        }

        private Produto produtoRelacionadoAoEstoque;
        public Produto ProdutoRelacionadoAoEstoque
        {
            get => produtoRelacionadoAoEstoque;
            set => SetProperty(ref produtoRelacionadoAoEstoque, value);
        }

        private List<Categoria> categorias;
        public List<Categoria> Categorias
        {
            get => categorias;
            set => SetProperty(ref categorias, value);
        }

        private Categoria categoriaSelecionada;
        public Categoria CategoriaSelecionada
        {
            get => categoriaSelecionada;
            set => SetProperty(ref categoriaSelecionada, value);
        }

        private string novaQuantidade;
        public string NovaQuantidade
        {
            get => novaQuantidade;
            set => SetProperty(ref novaQuantidade, value);
        }

        public ICommand AtualizarCommand { get; }
        public ICommand CancelarCommand { get; }

        public EditEstoqueViewModel(Estoque estoqueRecebido) 
        {
            EstoqueSelecionado = estoqueRecebido;
            ProdutoRelacionadoAoEstoque = DataBaseManager.Instance.ObterProdutoPorNome(EstoqueSelecionado?.Produto);

            Categorias = DataBaseManager.Instance.ObterTodasCategorias();
            CategoriaSelecionada = Categorias.FirstOrDefault(c => c.Descricao == ProdutoRelacionadoAoEstoque.Categoria);

            AtualizarCommand = new RelayCommand(AtualizarEstoque);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void Cancelar()
        {
            if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this) is Window janela)
            {
                janela.Close();
            }
        }

        private void AtualizarEstoque()
        {

            if (!ValidarCampos())
            {
                MessageBox.Show("Preencha todos os campos obrigatórios!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EstoqueSelecionado.QuantidadeDisponivel = Convert.ToInt32(NovaQuantidade);

            DataBaseManager.Instance.AtualizarQuantidadeEstoque(EstoqueSelecionado);

            MessageBox.Show("Nova quantidade atualizada!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this) is Window janela)
            {
                janela.DialogResult = true;
                janela.Close();
            }
        }

        private bool ValidarCampos()
        {
            return int.TryParse(NovaQuantidade, out int quantidade) && quantidade > 0;
        }

    }
}
