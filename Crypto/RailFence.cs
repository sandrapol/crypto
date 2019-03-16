using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class RailFence
    {
        private string plainText;
        private string cipherText;
        private int key;
        private char[,] codeTable;

        public string Encrypt(string plainText, int key)
        {
            List<char>[] list = new List<char>[key];

            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new List<char>();
            }

            plainText = plainText.Replace(" ", "");

            int iterator = -1;
            int helper = 1;

            for (int i = 0; i < plainText.Length; i++)
            {
                iterator += helper;

                list[iterator].Add(plainText[i]);

                if (iterator == key-1)
                {
                    helper = -1;
                }
                else if (iterator == 0)
                {
                    helper = 1;
                }
            }

            string cipherText = "";

            for (int i = 0; i < list.Length; i++)
            {

                while (list[i].Count != 0)
                {
                    cipherText += list[i].First();
                    list[i].RemoveAt(0);
                }
            }

            return cipherText;
        }

        private string Decrypt()
        {
            string plainText = "aa";

            return plainText;
        }
    }
}
