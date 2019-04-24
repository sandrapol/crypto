using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class LFSR
    {
        public string Encrypt(string LFSRdegree, int lfsrLength, ref string iterations)
        {
            string output = "";
            LFSRdegree = string.Join("", LFSRdegree.Split(' '));

            List<byte> polynomialDegrees = new List<byte>();

            foreach (char x in LFSRdegree)
            {
                polynomialDegrees.Add(byte.Parse(x.ToString()));
            }

            Random rnd = new Random();

            var seedLength = polynomialDegrees.Max();
            List<byte> seed = new List<byte>();

            for (int i = 0; i < seedLength; i++)
            {
                seed.Add((byte)rnd.Next(2));
            }

            do
            {
                byte result = seed[polynomialDegrees[0] - 1];

                for (int i = 1; i < polynomialDegrees.Count; i++)
                {
                    byte a = seed[polynomialDegrees[i] - 1];

                    result = XOR(a, result);
                }

                foreach (var s in seed)
                {
                    iterations += s;
                }
                iterations += "\n";

                output += seed[seedLength - 1];
                seed.RemoveAt(seedLength - 1);
                seed.Insert(0, result);

            } while (--lfsrLength > 0);

            return output;
        }

        byte XOR(byte a, byte b)
        {
            if (a == b)
                return 0;
            else return 1;
        }
    }  
}
