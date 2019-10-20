using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Compress
{
    class CompNodeComparer : Comparer<CompNodeBase>
    {
        public override int Compare([AllowNull] CompNodeBase x, [AllowNull] CompNodeBase y)
        {
            return x.Count().CompareTo(y.Count());
        }
    }
}
