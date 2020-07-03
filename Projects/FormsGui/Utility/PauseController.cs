using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public class PauseController
   {
      public PauseController()
      {
         m_PauseEvent = new ManualResetEvent(true);
         m_AbortCommanded = false;
      }

      public void AbortChildProcess()
      {
         m_AbortCommanded = true;
         ResumeChildExecution();
      }

      public void WaitIfPauseCommanded()
      {
         m_PauseEvent.WaitOne();

         if (m_AbortCommanded)
         {
            m_AbortCommanded = false;
            throw new Simulation.Exceptions.AbortSignal();
         }
      }

      public void PauseChildTaskExecution()
      {
         m_PauseEvent.Reset();
      }

      public void ResumeChildExecution()
      {
         m_PauseEvent.Set();
      }

      private bool m_AbortCommanded;
      private readonly ManualResetEvent m_PauseEvent;
   }
}
