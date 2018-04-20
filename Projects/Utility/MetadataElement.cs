namespace Assembler.Common
{
    public class MetadataElement
    {
        public MetadataElement(ObjectTypeCode typeCode, int size)
        {
            m_TypeCode = typeCode;
            m_Size = size;
        }

        public ObjectTypeCode TypeCode
        {
            get { return m_TypeCode; }
        }

        public int Size
        {
            get { return m_Size; }
        }

        private readonly ObjectTypeCode m_TypeCode;
        private readonly int m_Size;
    }
}
