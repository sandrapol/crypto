using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class RailFence
    {
        public string Encrypt(string plainText, int key)
        {
            if (key == 1)
                return plainText;

            List<string> list = new List<string>();

            for (int i = 0; i < key; i++)
            {
                list.Add("");
            }

            int number = 0;
            int increment = 1;

            foreach (char c in plainText)
            {
                if (number + increment == key)
                {
                    increment = -1;
                }
                else if (number + increment == -1)
                {
                    increment = 1;
                }

                list[number] += c;
                number += increment;
            }

            string cipherText = "";

            foreach (string s in list)
            {
                cipherText += s;
            }

            return cipherText;
        }

        public string Decrypt(string cipherText, int key)
        {
            if (key == 1)
                return cipherText;

            List<List<int>> list = new List<List<int>>();

            for (int i = 0; i < key; i++)
            {
                list.Add(new List<int>());
            }

            int cipherLength = cipherText.Length;
            int number = 0;
            int increment = 1;

            for (int i = 0; i < cipherLength; i++)
            {
                if (number + increment == key)
                {
                    increment = -1;
                }
                else if (number + increment == -1)
                {
                    increment = 1;
                }

                list[number].Add(i);
                number += increment;
            }

            int counter = 0;
            char[] plainText = new char[cipherLength];

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    plainText[list[i][j]] = cipherText[counter];
                    counter++;
                }
            }

            return new string(plainText);
        }
    }
}
