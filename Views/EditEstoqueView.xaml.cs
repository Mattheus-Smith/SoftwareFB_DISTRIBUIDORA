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
    /// Lógica interna para EditEstoqueView.xaml
    /// </summary>
    public partial class EditEstoqueView : Window
    {
        private Estoque _estoque;
        private Produto _produto;
        private List<Categoria> categorias;

        public EditEstoqueView(Estoque estoque = null)
        {
            InitializeComponent();
            _estoque = estoque;
            ExibirProduto();
            CarregarCategorias();

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

        private void ExibirProduto()
        {
            _produto = DataBaseManager.Instance.ObterProdutoPorNome(_estoque.Produto);

            txtDescricao.Text = _produto.NomeProduto;
            txtPrecoUnitario.Text = _produto.PrecoUnitario.ToString();
            txtPrecoVenda.Text = _produto.PrecoVenda.ToString();
            txtQuantidadeAtual.Text = _estoque.QuantidadeDisponivel.ToString();
        }

        private void Atualizar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                Estoque novoEstoque = new Estoque
                {
                    Id = _estoque.Id,
                    Unidade = _estoque.Unidade,
                    QuantidadeDisponivel = Convert.ToInt32(txtNovaQuantidade.Text),
                    UltimaAlteracao = DateTime.Now,
                };

                DataBaseManager.Instance.AtualizarQuantidadeEstoque(novoEstoque);

                MessageBox.Show("Nova quantidade atualizada!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // para indicar sucesso ao chamar via ShowDialog()
                this.Close();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNovaQuantidade.Text))
            {
                MessageBox.Show("Preencha nova quantidade a ser adicionada no estoque!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
