using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public class Trie
   {
      private class Node
      {
         public Node(char val, int depth, Node parent)
         {
            m_Children = new List<Node>();
            m_Value = val;
            m_Depth = depth;
            m_Parent = parent;
         }

         public bool IsLeaf
         {
            get { return m_Children.Count == 0; }
         }

         public char Value
         {
            get { return m_Value; }
         }

         public int Depth
         {
            get { return m_Depth; }
         }

         public Node Parent
         {
            get { return m_Parent; }
         }

         public IEnumerable<Node> ChildNodes
         {
            get { return m_Children; }
         }

         public void AddChildNode(Node c)
         {
            m_Children.Add(c);
         }
         

         public Node FindChildNode(char c)
         {
            foreach (Node child in m_Children)
            {
               if (child.Value == c)
               {
                  return child;
               }
            }

            return null;
         }

         public void DeleteChildNode(char c)
         {
            for (int i = 0; i < m_Children.Count; ++i)
            {
               if (m_Children[i].Value == c)
               {
                  m_Children.RemoveAt(i);
               }
            }
         }

         private readonly List<Node> m_Children;
         private readonly Node m_Parent;
         private readonly char m_Value;
         private readonly int m_Depth;
      }

      public Trie()
      {
         m_RootNode = new Node('\0', 0, null);
      }

      private Node GetLastMatchedNodeOfString(string str)
      {
         Node currNode = m_RootNode;

         var result = currNode;


         foreach (char c in str)
         {
            currNode = currNode.FindChildNode(c);
            if (currNode == null)
            {
               break;
            }

            result = currNode;
         }

         return result;
      }

      private Node GetNodeMatchingTerminalCharacter(string str)
      {
         Node currNode = m_RootNode;

         Node result = currNode;
         foreach (char c in str)
         {
            currNode = currNode.FindChildNode(c);
            if (currNode == null)
            {
               result = m_RootNode;
               break;
            }

            result = currNode;
         }

         return result;
      }

      public bool DoesStringExist(string s)
      {
         var strPrefix = GetLastMatchedNodeOfString(s);
         return strPrefix.Depth == s.Length;
      }

      /// <summary>
      /// Inserts multiple strings into the trie.
      /// </summary>
      /// <param name="items"></param>
      public void InsertRange(IEnumerable<string> items)
      {
         foreach (string item in items)
         {
            Insert(item);
         }
      }

      public IEnumerable<string> GetAllPossibleStrings(string startingStr)
      {
         Node commonPrefix = GetNodeMatchingTerminalCharacter(startingStr);
         IEnumerable<string> strList;
         if (commonPrefix.Value == '\0')
         {
            strList = new List<string>();
         }
         else
         {
            var strBuilder = new StringBuilder();
            strBuilder.Append(startingStr);
            strList = BuildStrings(commonPrefix, strBuilder);
         }

         return strList;
      }

      private IEnumerable<string> BuildStrings(Node startingNode, StringBuilder strBuilder)
      {
         var strList = new List<string>();
         if (startingNode.IsLeaf)
         {
            string finalString = strBuilder.ToString();
            strList.Add(finalString);
         }
         else
         {
            foreach (Node childNode in startingNode.ChildNodes)
            {
               RecursivelyBuildStrings(childNode, strBuilder, ref strList);
            }
         }

         return strList;
      }

      private void RecursivelyBuildStrings(Node startingNode, StringBuilder strBuilder, ref List<string> stringList)
      {
         strBuilder.Append(startingNode.Value);
         if (startingNode.IsLeaf)
         {
            string finalString = strBuilder.ToString();
            stringList.Add(finalString);
         }
         else
         {
            foreach (Node childNode in startingNode.ChildNodes)
            {
               RecursivelyBuildStrings(childNode, strBuilder, ref stringList);
            }
         }

         strBuilder.PopCharacter();
      }

      /// <summary>
      /// Inserts a string into the trie.
      /// </summary>
      /// <param name="s">The string to insert.</param>
      public void Insert(string s)
      {
         var commonPrefix = GetLastMatchedNodeOfString(s);
         var current = commonPrefix;

         for (int i = current.Depth; i < s.Length; ++i)
         {
            var newNode = new Node(s[i], current.Depth + 1, current);
            current.AddChildNode(newNode);
            current = newNode;
         }
      }

      private readonly Node m_RootNode;

   }
}
