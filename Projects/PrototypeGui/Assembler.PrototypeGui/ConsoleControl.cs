using Assembler.Common;
using Assembler.PrototypeGui.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.PrototypeGui
{
    public partial class ConsoleControl : UserControl
    {
        private readonly LogConsole m_OutputLogs;
        public ConsoleControl()
        {
            InitializeComponent();
            m_OutputLogs = new LogConsole();
            m_OutputLogs.LogOutput.ListChanged += LogOutput_ListChanged;
        }

        public ILogger Logger
        {
            get { return m_OutputLogs; }
        }

        private void LogOutput_ListChanged(object sender, ListChangedEventArgs e)
        {
            var logList = sender as BindingList<string>;
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                m_LogList.Items.Add(logList[e.NewIndex]);
            }
        }
    }
}
