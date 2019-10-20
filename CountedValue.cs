namespace Compress
{
    struct CountedValue
    {
        public CountedValue(byte value, int count)
        {
            Value = value;
            Count = count;
        }

        public byte Value;
        public int Count;
    }
}
