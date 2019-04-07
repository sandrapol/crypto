using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class LFSRuse
    {
        static void UseLFSR(string[] args)
        {
            Dictionary<int, bool> tester = new Dictionary<int, bool>();
            List<int> duplicate = new List<int>();

            LFSR register_1 = new LFSR(8, new int[] { 5, 4, 3 });

            for (int index = 0; index < 256; index++)
            {
                bool b00 = register_1.Clock();
                bool b01 = register_1.Clock();
                bool b02 = register_1.Clock();
                bool b03 = register_1.Clock();
                bool b04 = register_1.Clock();
                bool b05 = register_1.Clock();
                bool b06 = register_1.Clock();
                bool b07 = register_1.Clock();

                BitArray bitArray = new BitArray(new bool[] { b00, b01, b02, b03, b04, b05, b06, b07 });
                int[] array = new int[1];
                bitArray.CopyTo(array, 0);

                if (!tester.ContainsKey(array[0]))
                    tester.Add(array[0], true);
                else
                    duplicate.Add(array[0]);
            }

            duplicate = duplicate.OrderBy(x => x).ToList();
        }
    }
}
