using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class DataAddressViewModel : BaseViewModel, IDisposable
   {
      public DataAddressViewModel(int startingSegmentAddress,
                                  DataModels.BindableDataSegmentAccessor segmentAccessor)
      {
         m_StartingOffset = startingSegmentAddress;
         m_DisplayType = RegisterDisplayType.Hexadecimal;
         m_MaxOffset = m_StartingOffset + (4 * WORD_SIZE_BYTES);
         m_Accessor = segmentAccessor;
         m_Accessor.DataSegmentWritten += OnDataSegmentChanged;
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }
      
      public RegisterDisplayType DisplayType
      {
         get { return m_DisplayType; }
         set
         {
            if (m_DisplayType != value)
            {
               m_DisplayType = value;
               OnPropertyChanged();
               OnPropertyChanged(nameof(Word0Str));
               OnPropertyChanged(nameof(Word1Str));
               OnPropertyChanged(nameof(Word2Str));
               OnPropertyChanged(nameof(Word3Str));
            }
         }
      }

      public string BaseAddressStr
      {
         get { return "0x" + m_StartingOffset.ToString("x8"); }
      } 

      public string Word0Str
      {
         get
         {
            string dispStr = string.Empty;
            switch (m_DisplayType)
            {
               case RegisterDisplayType.Decimal:
               {
                  dispStr = Word0.ToString();
                  break;
               }

               case RegisterDisplayType.Hexadecimal:
               {
                  dispStr = "0x" + Word0.ToString("x8");
                  break;
               }
            }

            return dispStr;
         }
         set
         {
            if (Common.IntExtensions.TryParseEx(value, out int iVal))
            {
               if (Word0 != iVal)
               {
                  Word0 = iVal;
                  OnPropertyChanged();
               }
            }
         }
      }

      public int Word0
      {
         get { return m_Accessor.ReadWord(m_StartingOffset); }
         set
         {
            m_Accessor.WriteWord(m_StartingOffset, value);
         }
      }

      public string Word1Str
      {
         get
         {
            string dispStr = string.Empty;
            switch (m_DisplayType)
            {
               case RegisterDisplayType.Decimal:
               {
                  dispStr = Word1.ToString();
                  break;
               }

               case RegisterDisplayType.Hexadecimal:
               {
                  dispStr = "0x" + Word1.ToString("x8");
                  break;
               }
            }

            return dispStr;
         }
         set
         {
            if (Common.IntExtensions.TryParseEx(value, out int iVal))
            {
               if (Word1 != iVal)
               {
                  Word1 = iVal;
                  OnPropertyChanged();
               }
            }
         }
      }

      public int Word1
      {
         get { return m_Accessor.ReadWord(m_StartingOffset + WORD_SIZE_BYTES); }
         set
         {
            m_Accessor.WriteWord(m_StartingOffset + WORD_SIZE_BYTES, value);
         }
      }

      public string Word2Str
      {
         get
         {
            string dispStr = string.Empty;
            switch (m_DisplayType)
            {
               case RegisterDisplayType.Decimal:
               {
                  dispStr = Word2.ToString();
                  break;
               }

               case RegisterDisplayType.Hexadecimal:
               {
                  dispStr = "0x" + Word2.ToString("x8");
                  break;
               }
            }

            return dispStr;
         }
         set
         {
            if (Common.IntExtensions.TryParseEx(value, out int iVal))
            {
               if (Word2 != iVal)
               {
                  Word2 = iVal;
                  OnPropertyChanged();
               }
            }
         }
      }

      public int Word2
      {
         get { return m_Accessor.ReadWord(m_StartingOffset + (2 * WORD_SIZE_BYTES)); }
         set
         {
            m_Accessor.WriteWord(m_StartingOffset + (2 * WORD_SIZE_BYTES), value);
         }
      }

      public string Word3Str
      {
         get
         {
            string dispStr = string.Empty;
            switch (m_DisplayType)
            {
               case RegisterDisplayType.Decimal:
               {
                  dispStr = Word3.ToString();
                  break;
               }

               case RegisterDisplayType.Hexadecimal:
               {
                  dispStr = "0x" + Word3.ToString("x8");
                  break;
               }
            }

            return dispStr;
         }
         set
         {
            if (Common.IntExtensions.TryParseEx(value, out int iVal))
            {
               if (Word3 != iVal)
               {
                  Word3 = iVal;
                  OnPropertyChanged();
               }
            }
         }
      }

      public int Word3
      {
         get { return m_Accessor.ReadWord(m_StartingOffset + (3 * WORD_SIZE_BYTES)); }
         set
         {
            m_Accessor.WriteWord(m_StartingOffset + (3 * WORD_SIZE_BYTES), value);
         }
      }

      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            m_Accessor.DataSegmentWritten -= OnDataSegmentChanged;
         }
      }

      private void OnDataSegmentChanged(object sender, DataModels.DataSegmentChangedEventArgs e)
      {
         // did the changed address affect one of the four words we care about?
         int maxAffectedAddress = e.StartingAddress + (e.NumWordsUpdated * WORD_SIZE_BYTES);

         // if either of the beginning or ending values of the range lie within the range specified by the
         // event (or if we encompass the entire range), update all word values on the model.
         if ((e.StartingAddress <= m_StartingOffset && m_StartingOffset < maxAffectedAddress) ||
             (e.StartingAddress <= m_MaxOffset && m_MaxOffset < maxAffectedAddress) ||
             (m_StartingOffset <= e.StartingAddress && m_MaxOffset >= maxAffectedAddress))
         {
            OnPropertyChanged(nameof(Word0Str));
            OnPropertyChanged(nameof(Word1Str));
            OnPropertyChanged(nameof(Word2Str));
            OnPropertyChanged(nameof(Word3Str));
         }
      }

      private const int WORD_SIZE_BYTES = 4;
      private readonly int m_StartingOffset;
      private readonly int m_MaxOffset;
      private RegisterDisplayType m_DisplayType;
      private readonly DataModels.BindableDataSegmentAccessor m_Accessor;
   }
}
