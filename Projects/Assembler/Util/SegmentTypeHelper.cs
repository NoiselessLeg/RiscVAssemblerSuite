using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{

    class SegmentTypeHelper
    {
        /// <summary>
        /// Creates the segment types database.
        /// </summary>
        static SegmentTypeHelper()
        {
            s_SegmentTypes = new Dictionary<string, SegmentType>()
            {
                { ".data", SegmentType.Data },
                { ".text", SegmentType.Text }
            };
        }

        /// <summary>
        /// Determines if a segment declaration is mapped to a currently supported
        /// segment type.
        /// </summary>
        /// <param name="token">The string token to examine.</param>
        /// <returns>True if the token declares the start of a new segment type.</returns>
        public static bool IsSegmentDeclarationToken(string token)
        {
            return s_SegmentTypes.ContainsKey(token);
        }

        /// <summary>
        /// Gets the segment type associated with a specific token.
        /// </summary>
        /// <param name="type">The token to examine.</param>
        /// <returns>The machine segment type associated with the token if it is a 
        /// segment declaration, otherwise returns SegmentType.Invalid.</returns>
        public static SegmentType GetSegmentType(string type)
        {
            SegmentType segmentType = SegmentType.Invalid;

            if (!s_SegmentTypes.TryGetValue(type, out segmentType))
            {
                segmentType = SegmentType.Invalid;
            }

            return segmentType;
        }

        private static readonly Dictionary<string, SegmentType> s_SegmentTypes;
    }
}
