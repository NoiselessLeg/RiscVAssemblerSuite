using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class ParseException : Exception
   {
      public ParseException(string error):
         base(error)
      {
      }
   }

   public class ParsedCommand
   {
      public static ParsedCommand ParseInput(string commandLine)
      {
         string trimmedLine = commandLine.Trim();

         // see if our command contains an argument list.

         ParsedCommand retVal = null;
         if (trimmedLine.Contains('('))
         {
            if (!trimmedLine.Contains(')'))
            {
               throw new ParseException("Expected closing ')' character.");
            }

            // now that we split off the main command, try to parse our argument list
            int paramListStart = trimmedLine.IndexOf('(');
            string mainCmd = trimmedLine.Substring(0, paramListStart);
            int paramListEnd = trimmedLine.LastIndexOf(')');
            int argListLength = paramListEnd - paramListStart;

            // get the args without the ( and ) characters
            string args = trimmedLine.Substring(paramListStart + 1, argListLength - 1);

            string[] argArray = args.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // account for an empty parameter list
            if (argArray.Length > 0)
            {
               // trim the whitespace off of each argument, and replace the array value
               for (int i = 0; i < argArray.Length; ++i)
               {
                  argArray[i] = argArray[i].Trim();
               }

               retVal = new ParsedCommand(mainCmd, argArray);
            }
            else
            {
               retVal = new ParsedCommand(mainCmd);
            }
         }
         else
         {
            string[] tokens = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 1)
            {
               retVal = new ParsedCommand(tokens[0]);
            }
            else if (tokens.Length > 1)
            {
               string[] args = tokens.SubArray(1, tokens.Length - 1);
               retVal = new ParsedCommand(tokens[0], args);
            }
         }

         return retVal;
      }

      public string Command => m_Cmd;

      public int NumArguments => m_NumArgs;

      public string[] Arguments => m_Args;

      private ParsedCommand(string cmd)
      {
         m_Cmd = cmd;
         m_NumArgs = 0;
      }

      private ParsedCommand(string command, string[] args)
      {
         m_Cmd = command;
         m_Args = args;
         m_NumArgs = args.Length;
      }


      private readonly string m_Cmd;
      private readonly string[] m_Args;
      private readonly int m_NumArgs;
   }
}
