namespace Assembler.Common
{

   /// <summary>
   /// Represents a symbol in an assembly file.
   /// </summary>
   public class Symbol
   {
      /// <summary>
      /// This constant defines the "unresolved" or null address.
      /// </summary>
      public const int UNRESOLVED_ADDR = 0;

      /// <summary>
      /// Creates a new instance of an assembly symbol.
      /// </summary>
      /// <param name="name">The name of the label.</param>
      /// <param name="foundMemType">Which segment type the label was found in</param>
      /// <param name="address">The relative address of the label in the .text/.data segment.</param>
      public Symbol(string name, SegmentType foundMemType, int address)
      {
         m_SegType = foundMemType;
         m_LabelName = name;
         m_Address = address;
      }

      /// <summary>
      /// Determines if a symbol has been "resolved" by the assembler. This
      /// indicates that a valid address (i.e. non-null) is attached to it.
      /// </summary>
      public bool IsResolvedSymbol
      {
         get { return Address != UNRESOLVED_ADDR; }
      }

      /// <summary>
      /// Gets the type of memory that this symbol was found in (i.e.
      /// the .text segment or .data segment).
      /// </summary>
      public SegmentType SegmentType => m_SegType;

      /// <summary>
      /// Gets the name of the symbol.
      /// </summary>
      public string LabelName => m_LabelName;

      /// <summary>
      /// Gets or sets the address of the symbol.
      /// </summary>
      public int Address
      {
         get { return m_Address; }
         set
         {
            if (m_Address != value)
            {
               m_Address = value;
            }
         }
      }

      /// <summary>
      /// Gets the size of the underlying object the symbol represents, in bytes.
      /// </summary>
      public int Size
      {
         get { return m_SymSize; }
         set
         {
            if (m_SymSize == 0)
            {
               m_SymSize = value;
            }
            else
            {
               System.Diagnostics.Debug.Assert(false);
            }
         }
      }

      private readonly SegmentType m_SegType;
      private readonly string m_LabelName;
      private int m_SymSize;
      private int m_Address;
   }
}
