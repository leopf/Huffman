using System.Collections.Generic;

namespace Compress
{
    class CompEncoder
    {
        public void CreateTree(byte[] data, out CompNode tree, out CompNodeBase[] valueNodes)
        {
            CompNodeComparer nodeComparer = new CompNodeComparer();
            CountedValue[] countedValues = CountValues(data);
            List<CompNodeBase> nodes = new List<CompNodeBase>();
            foreach (CountedValue cv in countedValues)
            {
                nodes.Add(new CompNodeValue(cv.Value, cv.Count));
            }

            valueNodes = nodes.ToArray();

            while (nodes.Count > 1)
            {
                nodes.Add(new CompNode(nodes[0], nodes[1]));
                nodes.RemoveAt(0);
                nodes.RemoveAt(0);
                nodes.Sort(nodeComparer);
            }

            tree = nodes[0] as CompNode;
        }
        public byte[] DecodeTree(CompNode tree, bool[] data)
        {
            List<byte> result = new List<byte>();
            CompNode selectedNode = tree;
            foreach (bool singleBit in data)
            {
                CompNodeBase currentNode = selectedNode.Next(singleBit);
                if (currentNode is CompNodeValue vNode)
                {
                    result.Add(vNode.Value);
                    selectedNode = tree;
                }
                else
                {
                    selectedNode = currentNode as CompNode;
                }
            }
            return result.ToArray();
        }
        public bool[] EncodeTree(CompNodeValue[] valueNodes, byte[] data)
        {
            CompNodeValue[] cnMap = new CompNodeValue[255];
            foreach (CompNodeValue cnv in valueNodes)
            {
                cnMap[cnv.Value] = cnv;
            }

            List<bool> result = new List<bool>();
            foreach (byte v in data)
            {
                result.AddRange(cnMap[v].GetEncoding());
            }

            return result.ToArray();
        }

        private CountedValue[] CountValues(byte[] data)
        {
            int[] counts = new int[256];
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 0;
            }

            foreach (byte v in data)
            {
                counts[v]++;
            }

            int amountnotnull = 0;
            foreach (int c in counts)
            {
                if (c > 0)
                {
                    amountnotnull++;
                }
            }

            CountedValue[] cvs = new CountedValue[amountnotnull];
            int cvsindex = 0;
            for (int i = 0; i < 256; i++)
            {
                if (counts[i] > 0)
                {
                    cvs[cvsindex++] = new CountedValue((byte)i, counts[i]);
                }
            }

            return cvs;
        }
    }
}
