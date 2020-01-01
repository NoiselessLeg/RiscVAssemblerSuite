﻿using Assembler.FormsGui.Controls.Custom;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Views
{
   public interface IBasicView
   {
      MenuBarContext MenuBarMembers { get; }
      IBasicQueue<IBasicMessage> MessageQueue { get; }
   }
}