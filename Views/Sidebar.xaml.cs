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
    /// Interação lógica para Sidebar.xam
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public event Action<string> ItemSelecionado;

        public Sidebar()
        {
            InitializeComponent();

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView list && list.SelectedItem is ListViewItem item)
            {
                var tag = item.Tag?.ToString();
                if (!string.IsNullOrEmpty(tag))
                {
                    ItemSelecionado?.Invoke(tag);
                }
            }
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_comanda.Visibility = Visibility.Collapsed;
                tt_pdv.Visibility = Visibility.Collapsed;
                tt_vendas.Visibility = Visibility.Collapsed;
                tt_produto.Visibility = Visibility.Collapsed;
                tt_estoque.Visibility = Visibility.Collapsed;
                tt_configuracoes.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_comanda.Visibility = Visibility.Visible;
                tt_pdv.Visibility = Visibility.Visible;
                tt_vendas.Visibility = Visibility.Visible;
                tt_produto.Visibility = Visibility.Visible;
                tt_estoque.Visibility = Visibility.Visible;
                tt_configuracoes.Visibility = Visibility.Visible;
            }
        }

    }
}
