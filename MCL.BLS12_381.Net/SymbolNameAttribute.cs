using System;

namespace MCL.BLS12_381.Net
{
    class SymbolNameAttribute : Attribute
    {
        public readonly string Name;

        public SymbolNameAttribute(string name)
        {
            Name = name;
        }
    }
}