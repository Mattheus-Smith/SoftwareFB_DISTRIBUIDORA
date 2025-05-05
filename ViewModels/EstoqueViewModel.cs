using SoftwareFB_DISTRIBUIDORA.BancoDeDados;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    internal class EstoqueViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Estoque> Estoques { get; set; } //Notifica automaticamente a interface gráfica (UI) quando itens são adicionados, removidos ou a coleção é atualizada.

        public EstoqueViewModel()
        {
            CarregarEstoque();
        }

        private void CarregarEstoque()
        {
            var estoques = DataBaseManager.Instance.ObterTodosOsItensDoEstoque();
            Estoques = new ObservableCollection<Estoque>(estoques); //Estou atualizando a coleção de produtos
            OnPropertyChanged(nameof(Estoques)); //Avise a interface (UI) que a propriedade mudou.
        }

        public void AtualizarListaDosItensDoEstoque()
        {
            var estoques = DataBaseManager.Instance.ObterTodosOsItensDoEstoque();
            Estoques = new ObservableCollection<Estoque>(estoques);
            OnPropertyChanged(nameof(Estoques));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) //Esse método avisa a UI: "Ei, a propriedade Produtos mudou!".
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
