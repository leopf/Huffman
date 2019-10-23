using System;
using System.Collections;

namespace Compress
{
    class Program
    {
        static void Main(string[] args)
        {
            BitEncoder benc = new BitEncoder();
            CompEncoder cenc = new CompEncoder();

            Random random = new Random();

            byte[] testdata = new byte[random.Next(100, 1000)];
            random.NextBytes(testdata);

            cenc.CreateTree(testdata, out CompNode cTree, out CompNodeValue[] cValueNodes);
            
            bool[] encodedData = cenc.EncodeTree(cValueNodes, testdata);
            byte[] decodedData = cenc.DecodeTree(cTree, encodedData);

            Console.WriteLine("Success: {0}", IsDataEqual(decodedData, testdata));
            Console.ReadKey();
        }

        private static bool IsDataEqual(params byte[][] dataArrays)
        {
            if (dataArrays.Length == 0) return false;

            int length = dataArrays[0].Length;
            for (int i = 1; i < dataArrays.Length; i++)
            {
                if (length != dataArrays[i].Length) 
                {
                    return false;
                }
            }

            for (int k = 0; k < dataArrays.Length; k++)
            {
                for (int h = k + 1; h < dataArrays.Length; h++)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (dataArrays[k][i] != dataArrays[h][i]) 
                        {
                            return false;
                        }   
                    }
                }
            }

            return true;
        }
    }
}
