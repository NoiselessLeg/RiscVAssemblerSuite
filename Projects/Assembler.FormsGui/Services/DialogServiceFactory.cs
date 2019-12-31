using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Services
{
    public static class DialogServiceFactory
    {
        private static IDialogService s_Service;

        public static IDialogService GetServiceInstance()
        {
            if (s_Service == null)
            {
                s_Service = new Win32DialogService();
            }

            return s_Service;
        }
    }
}
