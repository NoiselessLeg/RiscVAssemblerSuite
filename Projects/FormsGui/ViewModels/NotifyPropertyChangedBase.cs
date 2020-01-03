using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assembler.FormsGui.ViewModels
{
   /// <summary>
   /// The base class for all view models. Implements a method that
   /// determines which property changed on the fly, and delegates to the
   /// PropertyChanged handler when a property changes.
   /// </summary>
   public class BaseViewModel : INotifyPropertyChanged
   {
      /// <summary>
      /// The event handler that is called when a property changes.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      /// Calls the PropertyChanged event handler with the caller name.
      /// </summary>
      /// <param name="propertyName">The name of the property that changed. This is automatically filled
      /// out by the compiler.</param>
      protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
         Type thisType = GetType();
         PropertyInfo[] propInfo = thisType.GetProperties();

         if (!propInfo.Contains(param => param.Name == propertyName))
         {
            throw new ArgumentException("\"" + propertyName + "\" is not a property defined by type " + thisType.ToString());
         }

         var handler = PropertyChanged;
         if (handler != null)
         {
            var args = new PropertyChangedEventArgs(propertyName);
            handler.Invoke(this, args);
         }
      }
   }
}
