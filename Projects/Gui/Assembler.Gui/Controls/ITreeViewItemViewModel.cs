using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.Controls
{
    public interface ITreeViewItemViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ITreeViewItemViewModel> Children { get; }
    }
}
