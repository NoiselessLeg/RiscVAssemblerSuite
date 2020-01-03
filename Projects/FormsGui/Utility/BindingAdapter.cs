using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public static class BindingAdapter
   {
      public static void CreateListBinding<TControlList, TSourceListElemType>(this TControlList list,
                                                                              BindingList<TSourceListElemType> sourceList)
         where TControlList : IList
      {
         list.Clear();
         foreach (var elem in sourceList)
         {
            list.Add(elem);
         }

         sourceList.ListChanged += (s, e) =>
         {
            var srcList = s as BindingList<TSourceListElemType>;
            switch (e.ListChangedType)
            {
               case ListChangedType.ItemAdded:
               {
                  list.Add(srcList[e.NewIndex]);
                  break;
               }
               case ListChangedType.ItemDeleted:
               {
                  list.RemoveAt(e.OldIndex);
                  break;
               }
               case ListChangedType.ItemMoved:
               {
                  list.RemoveAt(e.OldIndex);
                  list.Insert(e.NewIndex, sourceList[e.NewIndex]);
                  break;
               }
               case ListChangedType.Reset:
               {
                  list.Clear();
                  break;
               }

               default:
                  break;
            }
         };
      }
   }
}
