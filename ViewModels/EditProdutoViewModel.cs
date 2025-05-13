using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

public class EditProdutoViewModel : ObservableObject
{
    private Produto produtoSelecionado;
    public Produto ProdutoSelecionado
    {
        get => produtoSelecionado;
        set => SetProperty(ref produtoSelecionado, value);
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

    private List<bool> valoresAtivo = new() { true, false };
    public List<bool> ValoresAtivo
    {
        get => valoresAtivo;
        set => SetProperty(ref valoresAtivo, value);
    }

    private bool ativoSelecionado;
    public bool AtivoSelecionado
    {
        get => ativoSelecionado;
        set => SetProperty(ref ativoSelecionado, value);
    }

    public ICommand AtualizarCommand { get; }
    public ICommand CancelarCommand { get; }

    public EditProdutoViewModel(Produto produtoRecebido)
    {
        ProdutoSelecionado = produtoRecebido;
        Categorias = DataBaseManager.Instance.ObterTodasCategorias();
        CategoriaSelecionada = Categorias.FirstOrDefault(c => c.Descricao == ProdutoSelecionado.Categoria);
        AtivoSelecionado = ProdutoSelecionado.Ativo;

        AtualizarCommand = new RelayCommand(AtualizarProduto);
        CancelarCommand = new RelayCommand(Cancelar);
    }

    private void AtualizarProduto()
    {
        if (!ValidarCampos())
        {
            MessageBox.Show("Preencha todos os campos obrigatórios!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        ProdutoSelecionado.Categoria = CategoriaSelecionada?.Descricao;
        ProdutoSelecionado.Ativo = AtivoSelecionado;

        DataBaseManager.Instance.AtualizarProduto(ProdutoSelecionado);

        MessageBox.Show("Produto atualizado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

        if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this) is Window janela)
        {
            janela.DialogResult = true;
            janela.Close();
        }
    }

    private void Cancelar()
    {
        if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this) is Window janela)
        {
            janela.Close();
        }
    }

    private bool ValidarCampos()
    {
        return !string.IsNullOrWhiteSpace(ProdutoSelecionado?.NomeProduto) &&
               ProdutoSelecionado.PrecoUnitario > 0 &&
               ProdutoSelecionado.PrecoVenda > 0 &&
               CategoriaSelecionada != null;
    }
}
