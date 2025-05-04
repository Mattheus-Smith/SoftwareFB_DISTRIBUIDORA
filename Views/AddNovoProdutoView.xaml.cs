using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
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
        private readonly string _funcionalidade;
        private List<Categoria> categorias;

        public AddNovoProdutoView(string funcionalidade)
        {
            InitializeComponent();
            _funcionalidade = funcionalidade;
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            categorias = DataBaseManager.Instance.ObterTodasCategorias();
            comboCategoria.ItemsSource = categorias; // define a lista de objetos a ser usada como fonte do ComboBox.
            comboCategoria.DisplayMemberPath = "Descricao"; // define a propriedade que será exibida na interface (ex: "Descricao").
            comboCategoria.SelectedValuePath = "Id"; // define a propriedade que será retornada em SelectedValue.
        }


        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                Produto novoProduto = new Produto
                {
                    NomeProduto = txtDescricao.Text,
                    CodigoBarra = long.TryParse(txtCodigoBarra.Text, out var codigo) ? codigo : (long?)null,
                    Categoria = comboCategoria.SelectedValue.ToString(), // string descricao = ((Categoria)comboCategoria.SelectedItem).Descricao;
                    PrecoUnitario = float.TryParse(txtPrecoUnitario.Text, out var unitario) ? unitario : 0,
                    PrecoVenda = float.TryParse(txtPrecoVenda.Text, out var venda) ? venda : 0,
                    PorcentagemDeLucro = float.TryParse(txtLucro.Text, out var lucro) ? lucro : 0,
                    Ativo = true
                };

                DataBaseManager.Instance.AdicionarProduto(novoProduto);

                MessageBox.Show("Produto adicionado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // para indicar sucesso ao chamar via ShowDialog()
                this.Close();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDescricao.Text) ||
                string.IsNullOrWhiteSpace(txtPrecoUnitario.Text) ||
                string.IsNullOrWhiteSpace(txtPrecoVenda.Text) ||
                comboCategoria.SelectedItem == null)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
