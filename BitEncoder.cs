using System;
using System.IO;

namespace Compress
{
    public class BitEncoder
    {
        public byte[] Encode(bool[] bits)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(BitConverter.GetBytes(bits.Length), 0, 4);
                
                for (int i = 0; i < bits.Length; i += 8)
                {
                    stream.WriteByte(EncodeByte(bits, i));
                }

                return stream.ToArray();
            }
        }
        public bool[] Decode(byte[] data)
        {
            int index = 0;
            bool[] bitData = new bool[(data.Length - 4) * 8];
            for (int i = 4; i < data.Length; i++)
            {
                bool[] currentBits = DecodeByte(data[i]);
                Array.Copy(currentBits, 0, bitData, index, 8);
                index += 8;
            }

            int bitLength = BitConverter.ToInt32(data, 0);
            bool[] actualBitData = new bool[bitLength];
            Array.Copy(bitData, actualBitData, bitLength);

            return actualBitData;
        }

        private bool[] DecodeByte(byte val)
        {
            bool[] bits = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                bits[i] = ((1 << i) & val) != 0; 
            }
            return bits;
        }
        private byte EncodeByte(bool[] bits, int index)
        {
            byte val = 0;

            for (int i = 0; i < 8 && i + index < bits.Length; i++)
            {
                if (bits[index + i]) 
                {
                    val |= (byte)(1 << i);
                }
            }

            return val;
        }
    }
}
