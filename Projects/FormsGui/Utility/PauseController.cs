using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   /// <summary>
   /// Provides methods to control execution of a child process.
   /// </summary>
   public class PauseController
   {
      public PauseController()
      {
         m_PauseEvent = new ManualResetEvent(true);
         m_AbortCommanded = false;
      }
      
      /// <summary>
      /// This causes a waiting child process to terminate with a SIGABRT.
      /// The child process will reset this flag upon resumption.
      /// </summary>
      public void AbortChildProcess()
      {
         m_AbortCommanded = true;
         ResumeChildExecution();
      }

      /// <summary>
      /// If the parent process has commanded this process to pause, this will
      /// wait until the parent proces unpauses the task. Otherwise, this is effectively
      /// a no-op.
      /// </summary>
      public void WaitIfPauseCommanded()
      {
         m_PauseEvent.WaitOne();

         if (m_AbortCommanded)
         {
            m_AbortCommanded = false;
            throw new Simulation.Exceptions.AbortSignal();
         }
      }

      /// <summary>
      /// Pauses the execution of the child process.
      /// </summary>
      public void PauseChildTaskExecution()
      {
         m_PauseEvent.Reset();
      }

      /// <summary>
      /// Resumes execution of the child process.
      /// </summary>
      public void ResumeChildExecution()
      {
         m_PauseEvent.Set();
      }

      private bool m_AbortCommanded;
      private readonly ManualResetEvent m_PauseEvent;
   }
}
