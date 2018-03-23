using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.ViewModel
{
    class EditorPaneViewModel : ObservableObject
    {
        public EditorPaneViewModel()
        {
            m_CurrFile = new FileViewModel();
            m_DisplayedFiles = new ObservableCollection<FileViewModel>();
            m_DisplayedFiles.CollectionChanged += OnOpenFilesChanged;
            m_DisplayedFiles.Add(m_CurrFile);
        }

        public ObservableCollection<FileViewModel> OpenFiles
        {
            get
            {
                return m_DisplayedFiles;
            }
        }

        private void OnOpenFilesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {

            }
        }

        /// <summary>
        /// Event that is fired if a file is closed from the top of the pane.
        /// </summary>
        /// <param name="sender">The file that is requesting the close.</param>
        /// <param name="e">Unused.</param>
        private void OnOpenFileClosure(object sender, EventArgs e)
        {
            m_DisplayedFiles.Remove(sender as FileViewModel);
        }

        private readonly ObservableCollection<FileViewModel> m_DisplayedFiles;
        private FileViewModel m_CurrFile;
    }
}
