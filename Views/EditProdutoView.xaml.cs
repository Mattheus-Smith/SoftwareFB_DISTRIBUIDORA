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

namespace SoftwareFB_DISTRIBUIDORA.Views
{
    /// <summary>
    /// Lógica interna para EditProdutoView.xaml
    /// </summary>
    public partial class EditProdutoView : Window
    {
        private Produto _produto;
        private List<Categoria> categorias;

        public EditProdutoView(Produto produto = null)
        {
            InitializeComponent();
            _produto = produto;
            ExibirProdutos();
            CarregarCategorias();
            CarregarValoresAtivo();

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CarregarCategorias()
        {
            categorias = DataBaseManager.Instance.ObterTodasCategorias();
            comboCategoria.ItemsSource = categorias; // define a lista de objetos a ser usada como fonte do ComboBox.
            comboCategoria.DisplayMemberPath = "Descricao"; // define a propriedade que será exibida na interface (ex: "Descricao").
            comboCategoria.SelectedValuePath = "Id"; // define a propriedade que será retornada em SelectedValue.

            var categoriaSelecionada = categorias.FirstOrDefault(c => c.Descricao == _produto.Categoria);
            comboCategoria.SelectedItem = categoriaSelecionada; //exibe no ComboBox qual é o valor de Categoria
        }

        private void CarregarValoresAtivo()
        {
            var valoresAtivo = new List<bool> { true, false };
            comboAtivo.ItemsSource = valoresAtivo;

            comboAtivo.SelectedItem = _produto.Ativo; //exibe no ComboBox qual é o valor de Ativo

            //bool ativoSelecionado = comboAtivo.SelectedItem as bool? ?? false;
        }

        private void ExibirProdutos()
        {
            txtDescricao.Text = _produto.NomeProduto;
            txtCodigoBarra.Text = _produto.CodigoBarra.ToString();
            txtCodigoProduto.Text = _produto.Id.ToString();
            txtPrecoUnitario.Text = _produto.PrecoUnitario.ToString();
            txtPrecoVenda.Text = _produto.PrecoVenda.ToString();
            txtLucro.Text = _produto.PorcentagemDeLucro.ToString();
        }

        private void Atualizar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                Produto novoProduto = new Produto
                {
                    Id = Convert.ToInt32(txtCodigoProduto.Text),
                    NomeProduto = txtDescricao.Text,
                    CodigoBarra = long.TryParse(txtCodigoBarra.Text, out var codigo) ? codigo : (long?)null,
                    Categoria = comboCategoria.SelectedValue.ToString(), // string descricao = ((Categoria)comboCategoria.SelectedItem).Descricao;
                    PrecoUnitario = float.TryParse(txtPrecoUnitario.Text, out var unitario) ? unitario : 0,
                    PrecoVenda = float.TryParse(txtPrecoVenda.Text, out var venda) ? venda : 0,
                    PorcentagemDeLucro = float.TryParse(txtLucro.Text, out var lucro) ? lucro : 0,
                    Ativo = comboAtivo.SelectedItem as bool? ?? false
                };

                DataBaseManager.Instance.AtualizarProduto(novoProduto);

                MessageBox.Show("Produto atualizado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

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
