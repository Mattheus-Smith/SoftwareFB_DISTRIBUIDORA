using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
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
    /// Interação lógica para EstoqueView.xam
    /// </summary>
    public partial class EstoqueView : UserControl
    {
        public EstoqueView()
        {
            InitializeComponent();
            DataContext = new EstoqueViewModel();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Estoque estoqueSelecionado)
            {
                var novaJanela = new EditEstoqueView(estoqueSelecionado);

                bool? resultado = novaJanela.ShowDialog();

                if (resultado == true)
                {
                    // Produto foi editado, atualiza a lista
                    var viewModel = DataContext as EstoqueViewModel;
                    viewModel?.AtualizarListaDosItensDoEstoque();
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
