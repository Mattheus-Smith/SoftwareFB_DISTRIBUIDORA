using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using SoftwareFB_DISTRIBUIDORA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareFB_DISTRIBUIDORA.Views
{
    /// <summary>
    /// Interação lógica para ProdutoView.xam
    /// </summary>
    public partial class ProdutoView : UserControl
    {
        public ProdutoView()
        {
            InitializeComponent();

            DataContext = new ProdutoViewModel();

        }

        private void AbrirAddProduto_Click(object sender, RoutedEventArgs e)
        {
            var novaJanela = new AddNovoProdutoView("padrao");

            bool? resultado = novaJanela.ShowDialog();

            if (resultado == true)
            {
                // Produto foi adicionado, atualiza a lista
                var viewModel = DataContext as ProdutoViewModel;
                viewModel?.AtualizarListaProdutos();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Produto produtoSelecionado)
            {
                var novaJanela = new EditProdutoView(produtoSelecionado);

                bool? resultado = novaJanela.ShowDialog();

                if (resultado == true)
                {
                    // Produto foi editado, atualiza a lista
                    var viewModel = DataContext as ProdutoViewModel;
                    viewModel?.AtualizarListaProdutos();
                }
            }
            
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Código do produto ou nome...")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Código do produto ou nome...";
                tb.Foreground = Brushes.Gray;
            }
        }

    }
}
