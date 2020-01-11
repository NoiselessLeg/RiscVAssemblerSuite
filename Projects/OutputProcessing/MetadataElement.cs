using Assembler.Common;

namespace Assembler.OutputProcessing
{
   /// <summary>
   /// Represents an element of the metadata section in a .JEF file.
   /// </summary>
   public class MetadataElement
   {
      /// <summary>
      /// Constructs an instance of the metadata element.
      /// </summary>
      /// <param name="typeCode">The byte value representing the data type.</param>
      /// <param name="size">The size of the data (most likely used if it's a string type).</param>
      public MetadataElement(ObjectTypeCode typeCode, int size, int elemAlignment)
      {
         m_TypeCode = typeCode;
         m_Size = size;
         m_Alignment = elemAlignment;
      }

      /// <summary>
      /// Gets the byte code that identifies the type of the correlated object in the .data segment.
      /// </summary>
      public ObjectTypeCode TypeCode => m_TypeCode;

      /// <summary>
      /// Gets the size in bytes of the data element in the .data segment.
      /// </summary>
      public int Size => m_Size;

      /// <summary>
      /// Gets the alignment of the current data element in the .data segment.
      /// </summary>
      public int Alignment => m_Alignment;


      private readonly ObjectTypeCode m_TypeCode;
      private readonly int m_Size;
      private readonly int m_Alignment;
   }
}
