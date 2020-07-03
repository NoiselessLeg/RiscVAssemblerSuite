using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class TerminationManager
   {
      public TerminationManager(string loadedFilePath)
      {
         m_IsTerminated = false;
         m_IsRestarting = false;
         m_FileToLoad = loadedFilePath;
      }

      public bool IsTerminated
      {
         get { return m_IsTerminated; }
      }

      public bool IsSimulatorRestarting
      {
         get { return m_IsRestarting; }
      }
      
      public string AsmFileToLoad
      {
         get { return m_FileToLoad; }
      }
      

      public void RestartSimulator(string newFileToLoad)
      {
         m_FileToLoad = newFileToLoad;
         m_IsRestarting = true;
         m_IsTerminated = true;
      }

      public void Terminate()
      {
         m_IsTerminated = true;
      }


      private bool m_IsTerminated;
      private bool m_IsRestarting;
      private string m_FileToLoad;
   }
}
