namespace Assembler.Common
{

   /// <summary>
   /// Represents a symbol in an assembly file.
   /// </summary>
   public class Symbol
   {
      /// <summary>
      /// Creates a new instance of an assembly symbol.
      /// </summary>
      /// <param name="name">The name of the label.</param>
      /// <param name="foundMemType">Which segment type the label was found in</param>
      /// <param name="address">The relative address of the label in the .text/.data segment.</param>
      /// <param name="size">The size of the object the symbol demarcates, in bytes.</param>
      public Symbol(string name, SegmentType foundMemType, int address, int size)
      {
         m_SegType = foundMemType;
         m_LabelName = name;
         m_Address = address;
         m_SymSize = size;
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
      /// Gets the address this symbol was found in.
      /// </summary>
      public int Address => m_Address;

      /// <summary>
      /// Gets the size of the underlying object the symbol represents, in bytes.
      /// </summary>
      public int Size => m_SymSize;

      private readonly SegmentType m_SegType;
      private readonly string m_LabelName;
      private readonly int m_Address;
      private readonly int m_SymSize;
   }
}
