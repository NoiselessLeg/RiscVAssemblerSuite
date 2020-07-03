using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class CommandInterpreter
   {
      public CommandInterpreter()
      {
         m_CmdBuilder = new StringBuilder();
         m_CmdHistory = new List<string>();
         m_CmdTrie = new Trie();
         Console.TreatControlCAsInput = true;
         m_IsInsertModeEnabled = true;
      }

      public IEnumerable<string> GetSuggestedCommands(string typedCmd)
      {
         return m_CmdTrie.GetAllPossibleStrings(typedCmd);
      }

      public string ReadLine()
      {
         bool done = false;

         m_StartingRow = Console.CursorTop;
         m_StartingCol = Console.CursorLeft;

         while (!done)
         {
            // save this in case the user hits tab or something.
            int lastLeftPos = Console.CursorLeft;
            ConsoleKeyInfo cki = Console.ReadKey(true);

            if (cki.Modifiers == ConsoleModifiers.Control)
            {
               switch (cki.Key)
               {
                  case ConsoleKey.C:
                  {
                     throw new Simulation.Exceptions.InterruptSignal();
                  }

                  default:
                  {
                     Console.Write(cki.KeyChar);
                     m_CmdBuilder.Insert(m_CmdBuffIdx, cki.KeyChar);
                     ++m_CmdBuffIdx;
                     break;
                  }
               }

            }
            else
            {
               switch (cki.Key)
               {
                  case ConsoleKey.Enter:
                     done = true;
                     Console.Write(Environment.NewLine);
                     break;

                  case ConsoleKey.Backspace:
                     HandleBackspacePress();
                     break;

                  case ConsoleKey.Delete:
                     HandleDeletePress();
                     break;

                  case ConsoleKey.LeftArrow:
                     HandleLeftArrowPress();
                     break;

                  case ConsoleKey.RightArrow:
                     HandleRightArrowPress();
                     break;

                  case ConsoleKey.UpArrow:
                     HandleUpArrowPress();
                     break;

                  case ConsoleKey.DownArrow:
                     HandleDownArrowPress();
                     break;

                  default:
                     Console.Write(cki.KeyChar);
                     m_CmdBuilder.Insert(m_CmdBuffIdx, cki.KeyChar);
                     ++m_CmdBuffIdx;
                     break;
               }
            }
         }

         string finalizedCmd = m_CmdBuilder.ToString();
         m_CmdHistory.Add(finalizedCmd);

         m_CmdBuilder.Clear();
         m_CmdBuffIdx = 0;
         return finalizedCmd;
      }

      public string ReadCommand()
      {
         bool done = false;

         m_StartingRow = Console.CursorTop;
         m_StartingCol = Console.CursorLeft;
         
         while (!done)
         {
            // save this in case the user hits tab or something.
            int lastLeftPos = Console.CursorLeft;
            ConsoleKeyInfo cki = Console.ReadKey(true);

            if (cki.Modifiers == ConsoleModifiers.Control)
            {
               switch (cki.Key)
               {
                  case ConsoleKey.C:
                  {
                     Console.Write("Ctrl+C pressed. Type quit() to exit.\n");
                     Console.Write(">>> ");
                     break;
                  }

                  default:
                  {
                     Console.Write(cki.KeyChar);
                     m_CmdBuilder.Insert(m_CmdBuffIdx, cki.KeyChar);
                     ++m_CmdBuffIdx;
                     break;
                  }
               }

            }
            else
            {
               switch (cki.Key)
               {
                  case ConsoleKey.Enter:
                     done = true;
                     Console.Write(Environment.NewLine);
                     break;

                  case ConsoleKey.Backspace:
                     HandleBackspacePress();
                     break;

                  case ConsoleKey.Delete:
                     HandleDeletePress();
                     break;

                  case ConsoleKey.LeftArrow:
                     HandleLeftArrowPress();
                     break;

                  case ConsoleKey.RightArrow:
                     HandleRightArrowPress();
                     break;

                  case ConsoleKey.UpArrow:
                     HandleUpArrowPress();
                     break;

                  case ConsoleKey.DownArrow:
                     HandleDownArrowPress();
                     break;

                  case ConsoleKey.Tab:
                     HandleTabPress();
                     break;

                  case ConsoleKey.Insert:
                     m_IsInsertModeEnabled = !m_IsInsertModeEnabled;
                     break;

                  default:
                     if (!m_IsInsertModeEnabled)
                     {
                        if (m_CmdBuffIdx < m_CmdBuilder.Length)
                        {
                           m_CmdBuilder[m_CmdBuffIdx] = cki.KeyChar;
                        }
                        else
                        {
                           m_CmdBuilder.Append(cki.KeyChar);
                        }

                        ++m_CmdBuffIdx;
                        Console.Write(cki.KeyChar);
                     }
                     else
                     {
                        m_CmdBuilder.Insert(m_CmdBuffIdx, cki.KeyChar);
                        ++m_CmdBuffIdx;
                        Console.Write(cki.KeyChar);
                        uint numSpacesToMoveCursorBack = 0;
                        for (int i = m_CmdBuffIdx; i < m_CmdBuilder.Length; ++i)
                        {
                           Console.Write(m_CmdBuilder[i]);
                           ++numSpacesToMoveCursorBack;
                        }

                        DecrementConsoleCursorPosition(numSpacesToMoveCursorBack);

                     }
                     m_SuppressHistoryAddition = false;
                     break;
               }
            }
         }

         string finalizedCmd = m_CmdBuilder.ToString();

         if (!string.IsNullOrEmpty(finalizedCmd) && !m_SuppressHistoryAddition)
         {
            m_CmdHistory.Add(finalizedCmd);
         }

         m_SelectedHistoryIdx = m_CmdHistory.Count;
         m_SuppressHistoryAddition = false;
         m_CmdBuilder.Clear();
         m_CmdBuffIdx = 0;
         return finalizedCmd;
      }

      private void HandleDownArrowPress()
      {
         if (m_SelectedHistoryIdx < m_CmdHistory.Count)
         {
            ++m_SelectedHistoryIdx;
            string selectedCmd = string.Empty;
            if (m_SelectedHistoryIdx < m_CmdHistory.Count)
            {
               selectedCmd = m_CmdHistory[m_SelectedHistoryIdx];
            }
            ClearConsoleInput();
            Console.Write(selectedCmd);
            m_CmdBuilder.Clear();
            m_CmdBuilder.Append(selectedCmd);
            m_SuppressHistoryAddition = true;
         }
         else
         {
            ClearConsoleInput();
            m_CmdBuilder.Clear();
         }

         m_CmdBuffIdx = m_CmdBuilder.Length;
      }

      private void HandleUpArrowPress()
      {
         if (m_SelectedHistoryIdx > 0 && m_CmdHistory.Count > 0)
         {
            --m_SelectedHistoryIdx;

            string selectedCmd = string.Empty;
            if (m_SelectedHistoryIdx >= 0)
            {
               selectedCmd = m_CmdHistory[m_SelectedHistoryIdx];
            }
            ClearConsoleInput();
            Console.Write(selectedCmd);
            m_CmdBuilder.Clear();
            m_CmdBuilder.Append(selectedCmd);
            m_SuppressHistoryAddition = true;
         }
         m_CmdBuffIdx = m_CmdBuilder.Length;
      }

      private void HandleTabPress()
      {
         string currentTypedCmd = m_CmdBuilder.ToString();
         IEnumerable<string> possibleCmds = m_CmdTrie.GetAllPossibleStrings(currentTypedCmd);

         if (possibleCmds.Count() > 1)
         {
            Console.Write(Environment.NewLine);
            foreach (string cmd in possibleCmds)
            {
               Console.Write(cmd + Environment.NewLine);
            }

            Console.Write(">>> ");

            m_StartingRow = Console.CursorTop;
            m_StartingCol = Console.CursorLeft;
            Console.Write(currentTypedCmd);
            m_CmdBuffIdx = currentTypedCmd.Length;
         }
         else if (possibleCmds.Count() == 1)
         {
            ClearConsoleInput();
            m_CmdBuilder.Clear();

            string targetCmd = possibleCmds.ElementAt(0);
            m_CmdBuilder.Append(targetCmd);
            Console.Write(targetCmd);
            m_CmdBuffIdx = m_CmdBuilder.Length;
         }

         m_SuppressHistoryAddition = false;
      }

      private void HandleRightArrowPress()
      {
         // check to see if the buffer index is less than
         // our length. if so, increment our command char pointer.
         // don't bother incrementing our cursor since pressing ArrowRight
         // inserts an invisible character anyway.
         if (m_CmdBuffIdx < m_CmdBuilder.Length)
         {
            ++m_CmdBuffIdx;
            IncrementConsoleCursorPosition(1);
         }
      }

      public void AddAvailableCommands(IEnumerable<IConsoleCommand> availableCmds)
      {
         foreach (var cmd in availableCmds)
         {
            m_CmdTrie.Insert(cmd.CommandString);
         }
      }

      private void HandleLeftArrowPress()
      {
         if (m_CmdBuffIdx > 0)
         {
            --m_CmdBuffIdx;
            DecrementConsoleCursorPosition(1);
         }
      }

      private void HandleBackspacePress()
      {
         // the location where the user would start typing a line
         if (m_CmdBuffIdx > 0)
         {
            --m_CmdBuffIdx;

            int cmdBuffItr = m_CmdBuffIdx;
            m_CmdBuilder.Remove(m_CmdBuffIdx, 1);
            DecrementConsoleCursorPosition(1);

            int savedCursorTop = Console.CursorTop;
            int savedCursorLeft = Console.CursorLeft;

            while (cmdBuffItr < m_CmdBuilder.Length)
            {
               Console.Write(m_CmdBuilder[cmdBuffItr]);
               ++cmdBuffItr;
            }

            Console.Write(' ');

            Console.CursorTop = savedCursorTop;
            Console.CursorLeft = savedCursorLeft;


         }
      }

      private void HandleDeletePress()
      {
         // the location where the user would start typing a line
         if (m_CmdBuffIdx >= 0)
         {
            int cmdBuffItr = m_CmdBuffIdx;
            m_CmdBuilder.Remove(m_CmdBuffIdx, 1);

            int savedCursorTop = Console.CursorTop;
            int savedCursorLeft = Console.CursorLeft;

            while (cmdBuffItr < m_CmdBuilder.Length)
            {
               Console.Write(m_CmdBuilder[cmdBuffItr]);
               ++cmdBuffItr;
            }

            Console.Write(' ');

            Console.CursorTop = savedCursorTop;
            Console.CursorLeft = savedCursorLeft;


         }
      }

      private void IncrementConsoleCursorPosition(uint numIncrements)
      {
         while (numIncrements > 0)
         {
            if (Console.CursorLeft < Console.WindowWidth)
            {
               ++Console.CursorLeft;
               --numIncrements;
            }
            else
            {
               ++Console.CursorTop;
               Console.CursorLeft = 0;
               --numIncrements;
            }
         }
      }

      private void DecrementConsoleCursorPosition(uint numDecrements)
      {
         while (numDecrements > 0)
         {
            if (Console.CursorLeft > 0)
            {
               --Console.CursorLeft;
               --numDecrements;
            }
            else if (Console.CursorTop > 0)
            {
               --Console.CursorTop;
               Console.CursorLeft = Console.WindowWidth;
               --numDecrements;
            }
            else
            {
               numDecrements = 0;
            }
         }
      }

      private void ClearConsoleInput()
      {
         while (Console.CursorTop > m_StartingRow)
         {
            while (Console.CursorLeft > 0)
            {
               Console.Write(' ');
               DecrementConsoleCursorPosition(2);
            }
            Console.Write(' ');
            --Console.CursorTop;
            Console.CursorLeft = Console.WindowWidth;
         }

         while (Console.CursorLeft > m_StartingCol)
         {
            Console.Write(' ');
            DecrementConsoleCursorPosition(2);
         }

         Console.Write(' ');
         DecrementConsoleCursorPosition(1);
      }


      private int m_StartingRow;
      private int m_StartingCol;
      
      private int m_CmdBuffIdx;
      private bool m_IsInsertModeEnabled;
      private bool m_SuppressHistoryAddition;
      private int m_SelectedHistoryIdx;


      private readonly StringBuilder m_CmdBuilder;
      private readonly List<string> m_CmdHistory;
      private readonly Trie m_CmdTrie;
   }
}
