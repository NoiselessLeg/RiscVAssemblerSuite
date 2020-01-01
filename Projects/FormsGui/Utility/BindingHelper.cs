using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public static class BindingHelper
   {
      public static void CreateBinding<TControl, TSource, TControlProp>(this TControl control,
                                                                        TSource dataSource,
                                                                        Expression<Func<TControl, TControlProp>> controlProperty,
                                                                        string sourceProperty,
                                                                        Action<TControlProp, TSource> onChange)
         where TControl : System.Windows.Forms.Control
         where TSource : INotifyPropertyChanged
      {
         var compiledControlProp = controlProperty.Compile();

         TControlProp targetProp = compiledControlProp(control);

         dataSource.PropertyChanged += (s, e) =>
         {
            if (e.PropertyName == sourceProperty)
            {
               onChange(targetProp, dataSource);
            }
         };
         
         onChange(targetProp, dataSource);
      }
   }
}
