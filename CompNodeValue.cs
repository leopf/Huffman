using System.Collections.Generic;

namespace Compress
{
    class CompNodeValue : CompNodeBase
    {
        public CompNodeValue(byte value, int count)
        {
            DataValue = value;
            DataCount = count;
        }

        private byte DataValue;
        private int DataCount;
        private bool[] _Encoding = null;

        public byte Value { get => DataValue; }

        public override int Count()
        {
            return DataCount;
        }
        public bool[] GetEncoding()
        {
            if (_Encoding == null)
            {
                CompNodeBase current = this;
                List<bool> encoded = new List<bool>();
                while (current != null)
                {
                    encoded.Add(current.Parent.GetSelector(current));
                    current = current.Parent;
                }

                encoded.Reverse();
                _Encoding = encoded.ToArray();
            }

            return _Encoding;
        }
    }
}
