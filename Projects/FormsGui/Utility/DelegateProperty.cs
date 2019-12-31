using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public class DelegateProperty
   {

      public DelegateProperty(Predicate<object> predicate, object param)
      {
         m_PredicateProxy = predicate;
         m_Param = param;
      }

      public bool Value
      {
         get
         {
            return m_PredicateProxy(m_Param);
         }
      }
      private readonly Predicate<object> m_PredicateProxy;
      private readonly object m_Param;

   }

}
