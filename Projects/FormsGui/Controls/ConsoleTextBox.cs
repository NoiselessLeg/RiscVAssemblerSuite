using Assembler.Common;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public partial class ConsoleTextBox : UserControl
   {
      public ConsoleTextBox()
      {
         InitializeComponent();
         m_CurrUserCmd = string.Empty;
         m_Cmds = new Stack<string>();
         m_InputStream = new InputStream();
         m_OutputStream = new ObservableStream();
         m_OutputStream.OnDataWritten += OnStreamWrite;
      }

      public Stream InputStream
      {
         get
         {
            return m_InputStream;
         }
      }
      
      public Stream OutputStream
      {
         get { return m_OutputStream; }
      }

      public void ClearConsole()
      {
         m_InputStream.SetLength(0);
         m_CurrUserCmd = string.Empty;
         m_NumInputChars = 0;
         m_UnderlyingTxt.Clear();
      }
      
      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }

         if (disposing)
         {
            m_OutputStream.OnDataWritten -= OnStreamWrite;
            m_OutputStream.Close();
            m_InputStream.Close();
         }

         base.Dispose(disposing);
      }

      private bool IsDirectionKey(Keys keyCode)
      {
         bool isDirectionKey = false;
         switch (keyCode)
         {
            case Keys.Left:
            case Keys.Right:
            case Keys.Up:
            case Keys.Down:
            {
               isDirectionKey = true;
               break;
            }
         }

         return isDirectionKey;
      }

      private char ConvertToAsciiChar(Keys keyValue, bool isShiftApplied)
      {
         int iValue = (int)keyValue;

         char ret = (char)keyValue;
         if (isShiftApplied)
         {
            if ((int)Keys.A <= iValue && iValue <= (int)Keys.Z)
            {
               ret += (char)(keyValue + 32);
            }
         }

         else if ((int)Keys.NumPad0 <= iValue && iValue <= (int)Keys.NumPad9)
         {
            const int CONVERSION_FACTOR = ((int)Keys.NumPad0 - (int)Keys.D0);
            ret = (char)(iValue - CONVERSION_FACTOR);
         }

         return ret;
      }

      private void OnKeyDown(object sender, KeyEventArgs e)
      {
         if (!IsDirectionKey(e.KeyCode) && 
             e.KeyCode != Keys.Return &&
             e.KeyCode != Keys.Back)
         {
            char newChar = ConvertToAsciiChar(e.KeyCode, e.Shift);
            m_CurrUserCmd += newChar;
            ++m_NumInputChars;
         }
         else if (IsDirectionKey(e.KeyCode))
         {
            e.SuppressKeyPress = true;
         }
         else if (e.KeyCode == Keys.Return)
         {
            byte[] byteVal = Encoding.ASCII.GetBytes(m_CurrUserCmd);
            m_InputStream.Write(byteVal, 0, 1);
            m_CurrUserCmd = string.Empty;
            m_NumInputChars = 0;
         }
         else if (e.KeyCode == Keys.Back)
         {
            // this gets tricky. if something else other than our user
            // writes to the console, we need to reset the begin value so
            // the user doesn't backspace over output text. however,
            // if a user types something before other text shows up,
            // that text should be able to be removed from the buffer.
            // (think of a Linux terminal), although it may not be removed 
            // from the terminal window.
            if (m_CurrUserCmd.Length > 0)
            {
               m_CurrUserCmd = m_CurrUserCmd.Substring(0, m_CurrUserCmd.Length - 1);
            }

            // remove the last byte from the buffer (if anything is in there).
            if (m_InputStream.Length > 0)
            {
               m_InputStream.SetLength(m_InputStream.Length - 1);
            }

            // this should account for the case where text was inserted as we were typing.
            // this will cause NumInputchars to reset to 0.
            if (m_NumInputChars == 0)
            {
               e.SuppressKeyPress = true;
            }
            else
            {
               --m_NumInputChars;
            }
         }
      }

      private void OnStreamWrite(object sender, DataWrittenEventArgs e)
      {
         var stream = sender as Stream;
         byte[] writtenBytes = new byte[e.NumBytesWritten];
         stream.Read(writtenBytes, 0, e.NumBytesWritten);
         string value = Encoding.ASCII.GetString(writtenBytes);
         value = value.Replace("\n", Environment.NewLine);

         m_UnderlyingTxt.InvokeIfRequired(() =>
            {
               m_UnderlyingTxt.Text += value;
               m_NumInputChars = 0;
               m_UnderlyingTxt.SelectionStart = m_UnderlyingTxt.Text.Length;
            }
         );
      }

      private readonly InputStream m_InputStream;
      private readonly ObservableStream m_OutputStream;
      
      private readonly Stack<string> m_Cmds;

      private string m_CurrUserCmd;

      private int m_NumInputChars;
   }
}
