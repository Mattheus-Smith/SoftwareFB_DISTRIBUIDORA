using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
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
using SoftwareFB_DISTRIBUIDORA.ViewModels;

namespace SoftwareFB_DISTRIBUIDORA.Views
{
    /// <summary>
    /// Lógica interna para EditEstoqueView.xaml
    /// </summary>
    public partial class EditEstoqueView : Window
    {
        public EditEstoqueView(Estoque estoque)
        {
            InitializeComponent();
            DataContext = new EditEstoqueViewModel(estoque);
        }
    }
}
