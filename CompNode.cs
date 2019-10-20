using System;
using System.Linq;

namespace Compress
{
    class CompNode : CompNodeBase
    {
        public CompNode(params CompNodeBase[] subNodes)
        {
            if (subNodes.Length != 2)
            {
                throw new Exception("Invalid amount of subnodes.");
            }

            SubNodes = subNodes;

            foreach (CompNodeBase n in SubNodes)
            {
                n.Parent = this;
            }
        }

        private CompNodeBase[] SubNodes;

        private int? _Count = null;

        public bool GetSelector(CompNodeBase subNode)
        {
            int idx = Array.IndexOf(SubNodes, subNode);
            
            if (idx == -1) 
            {
                throw new Exception("Not the right parent node.");
            }

            return idx == 0 ? true : false;
        }
        public override int Count()
        {
            if (_Count == null)
            {
                _Count = SubNodes.Sum(n => n.Count());         
            }
            
            return _Count ?? 0;
        }
        public CompNodeBase Next(bool selector)
        {
            int index = selector ? 0 : 1;
            return SubNodes[index];
        }
    }
}
