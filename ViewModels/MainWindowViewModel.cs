using SoftwareFB_DISTRIBUIDORA.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            // Exibe uma view inicial, se quiser
            CurrentView = new ComandaViewModel();
        }

        // Método público para ser acessado pelo SidebarViewModel
        public void NavigateTo(ViewModelBase viewModel)
        {
            CurrentView = viewModel;
        }
    }

}
