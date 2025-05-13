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
using System.Windows.Shapes;

namespace SoftwareFB_DISTRIBUIDORA.Views
{
    /// <summary>
    /// Lógica interna para AddNovoProdutoView.xaml
    /// </summary>
    public partial class AddNovoProdutoView : Window
    {
        private List<Categoria> categorias;

        public AddNovoProdutoView()
        {
            InitializeComponent();

            var vm = new AddNovoProdutoViewModel();
            vm.FecharAction = Close;
            vm.SucessoAction = () => DialogResult = true;
            DataContext = vm;
        }

    }
}
