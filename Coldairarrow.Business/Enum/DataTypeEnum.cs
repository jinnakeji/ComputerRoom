using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Business.Enum
{
    public class DataTypeEnum
    {
        public enum Enum
        {
            @string,
            @int,
            @double
        }
        public string GetEnumName(Enum @enum)
        {
            switch (@enum)
            {
                case Enum.@string:
                    return "string";
                case Enum.@int:
                    return "int";
                case Enum.@double:
                    return "double";
            }
            return "string";
        }
    }
    
   
}
