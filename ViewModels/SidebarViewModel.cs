using SoftwareFB_DISTRIBUIDORA.Services;
using SoftwareFB_DISTRIBUIDORA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoftwareFB_DISTRIBUIDORA.ViewModels
{
    public class SidebarViewModel : ViewModelBase
    {
        public ICommand OpenComandaCommand { get; }

        private readonly Action<ViewModelBase> _navigateCallback;

        public SidebarViewModel(Action<ViewModelBase> navigateCallback)
        {
            _navigateCallback = navigateCallback;

            OpenComandaCommand = new RelayCommand(() =>
            {
                _navigateCallback.Invoke(new ComandaViewModel());
            });
        }
    }

}
