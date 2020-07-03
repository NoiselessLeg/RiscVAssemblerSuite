
using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Utility;
using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Utility
{
   public static class BindingHelper
   {
      public static void CreateBinding<TControl, TSource, TControlProp>(this TControl control,
                                                                        TSource dataSource,
                                                                        Expression<Func<TControl, TControlProp>> controlProperty,
                                                                        string sourceProperty,
                                                                        Action<TControlProp, TSource> onChange)
         where TControl : Control
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

      public static void BindToCommand(this Control btn, ICommand cmd)
      {
         btn.DataBindings.Add(new Binding(nameof(btn.Enabled), cmd, nameof(cmd.CanExecute), true, DataSourceUpdateMode.OnPropertyChanged));
         btn.Click += (s, e) => cmd.Execute(null);
      }


      public static void BindPredicateToEnabledProperty(ToolStripMenuItem item, ICommand cmd)
      {
         // data bindings don't seem to be working here?
         // oh well, let's manually bind to the event since that's probably what it's doing under the hood anyway.
         item.Enabled = cmd.CanExecute;
         cmd.PropertyChanged += (s, e) =>
         {
            var obj = s as ICommand;
            if (e.PropertyName == nameof(obj.CanExecute))
            {
               item.GetCurrentParent().Parent.InvokeIfRequired(() => item.Enabled = obj.CanExecute);
            }
         };
      }

      public static void BindToCommand(this ToolStripMenuItem btn, ICommand cmd)
      {
         BindPredicateToEnabledProperty(btn, cmd);
         btn.Click += (s, e) => cmd.Execute(null);
      }
   }
}
