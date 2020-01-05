using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Assembler.FormsGui.Utility
{
   public static class CollectionExtensions
   {
      public static void ApplyFunc<T>(this IEnumerable<T> enumerable, Func<T, object> lambda)
      {
         foreach (var item in enumerable)
         {
            lambda(item);
         }
      }

      public static bool Remove<T>(this ICollection<T> collection, int index)
      {
         bool canRemove = false;

         if (index < collection.Count)
         {
            var elem = collection.ElementAt(index);
            canRemove = collection.Remove(elem);
         }

         return canRemove;
      }

      /// <summary>
      /// Removes any item in an ICollection that matches a specific predicate.
      /// </summary>
      /// <typeparam name="T">The type contained by the ICollection instance</typeparam>
      /// <param name="collection">The collection to operate upon.</param>
      /// <param name="elemSelector">The Predicate that determines which items will be removed.</param>
      /// <returns>Returns true if any items matched the Predicate and were removed, otherwise returns false.</returns>
      public static bool RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> elemSelector)
      {
         bool ret = false;
         IEnumerable<T> subset = collection.Where(elemSelector);
         foreach (var elem in subset)
         {
            ret = true;
            collection.Remove(elem);
         }

         return ret;
      }

      public static bool Contains<T>(this IEnumerable<T> collection, Predicate<T> predicate)
      {
         bool found = false;
         IEnumerator<T> enumerator = collection.GetEnumerator();
         while (enumerator.MoveNext() && !found)
         {
            found = predicate(enumerator.Current);
         }

         return found;
      }

      public static int IndexOf<T>(this IEnumerable<T> collection, Predicate<T> predicate)
      {
         int foundIndex = -1;
         bool found = false;
         IEnumerator<T> enumerator = collection.GetEnumerator();
         while (enumerator.MoveNext() && !found)
         {
            ++foundIndex;
            found = predicate(enumerator.Current);
         }

         return foundIndex;
      }

      public static void BindToObservableCollection<TElemA, TElemB>(this IList<TElemA> collection,
                                                                    ObservableCollection<TElemB> collectionToBind,
                                                                    Func<TElemB, TElemA> mappingFunc)
      {
         collection.Clear();

         foreach (var elem in collectionToBind)
         {
            collection.Add(mappingFunc(elem));
         }

         collectionToBind.CollectionChanged += (s, e) =>
         {
            switch (e.Action)
            {
               case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
               {
                  foreach (TElemB newElem in e.NewItems)
                  {
                     TElemA newElemA = mappingFunc(newElem);
                     collection.Add(newElemA);
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
               {
                  if (e.NewStartingIndex != -1)
                  {
                     int index = e.NewStartingIndex;
                     foreach (TElemB newElem in e.NewItems)
                     {
                        TElemA elem = mappingFunc(newElem);
                        collection[index] = elem;
                        ++index;
                     }
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
               {
                  if (e.NewStartingIndex != -1)
                  {
                     int index = e.NewStartingIndex;
                     foreach (TElemB newElem in e.NewItems)
                     {
                        TElemA elem = mappingFunc(newElem);
                        collection[index] = elem;
                        ++index;
                     }
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
               {
                  int index = e.OldStartingIndex;
                  if (index != -1)
                  {
                     foreach (TElemB oldElem in e.OldItems)
                     {
                        collection.RemoveAt(index);
                        ++index;
                     }
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
               {
                  collection.Clear();
                  break;
               }
            }
         };
      }

      public static void BindToObservableCollection<TElem>(this IList collection,
                                                           ObservableCollection<TElem> collectionToBind,
                                                           Func<TElem, object> mappingFunc)
      {
         collection.Clear();

         foreach (var elem in collectionToBind)
         {
            collection.Add(mappingFunc(elem));
         }


         collectionToBind.CollectionChanged += (s, e) =>
         {
            switch (e.Action)
            {
               case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
               {
                  foreach (TElem newElem in e.NewItems)
                  {
                     object elem = mappingFunc(newElem);
                     collection.Add(elem);
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
               {
                  if (e.NewStartingIndex != -1)
                  {
                     int index = e.NewStartingIndex;
                     foreach (TElem newElem in e.NewItems)
                     {
                        object elem = mappingFunc(newElem);
                        collection[index] = elem;
                        ++index;
                     }
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
               {
                  if (e.NewStartingIndex != -1)
                  {
                     int index = e.NewStartingIndex;
                     foreach (TElem newElem in e.NewItems)
                     {
                        object elem = mappingFunc(newElem);
                        collection[index] = elem;
                        ++index;
                     }
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
               {
                  int index = e.OldStartingIndex;
                  if (index != -1)
                  {
                     foreach (TElem oldElem in e.OldItems)
                     {
                        collection.RemoveAt(index);
                        ++index;
                     }
                  }
                  break;
               }

               case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
               {
                  collection.Clear();
                  break;
               }
            }
         };
      }
   }
}
