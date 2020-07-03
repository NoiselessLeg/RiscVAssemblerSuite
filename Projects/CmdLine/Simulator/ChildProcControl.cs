using Assembler.Simulation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class ChildProcControl
   {
      public ChildProcControl()
      {
         m_ParentProcCtrl = new ManualResetEvent(true);
         m_ChildProcCtrl = new ManualResetEvent(false);
      }
      
      public void AbortChildProcess()
      {
         m_IsChildAborted = true;
         GrantControlToChildProcess();
         m_IsChildAborted = false;
      }

      public void GrantControlToChildProcess()
      {
         m_ParentProcCtrl.Reset();
         m_ChildProcCtrl.Set();
         m_ParentProcCtrl.WaitOne();
      }

      public void RelinquishControlToParentProcess()
      {
         m_ParentProcCtrl.Set();
         m_ChildProcCtrl.Reset();
         m_ChildProcCtrl.WaitOne();

         if (m_IsChildAborted)
         {
            throw new AbortSignal();
         }
      }

      private readonly ManualResetEvent m_ParentProcCtrl;
      private readonly ManualResetEvent m_ChildProcCtrl;
      private bool m_IsChildAborted;
   }
}
