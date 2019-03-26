using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Crypto
{
    class ColumnTransposition
    {
        private List<char[]> codeTable;
        private string key;
        private string plainText;
        private string result;
        private int[] indexes;

        public ColumnTransposition(string initial, string key)
        {
            this.plainText = initial.Replace(":", "").Replace(".", "").Replace(",", "").Replace(" ", "");
            this.result = "";
            this.key = key;
            codeTable = new List<char[]>();
            GetShiftedIndexes();
        }

        private void GetShiftedIndexes()
        {
            int keyLength = key.Length;
            indexes = new int[keyLength];
            List<KeyValuePair<int, char>> sortedKey = new List<KeyValuePair<int, char>>();
            int i;

            for (i = 0; i < keyLength; ++i)
            {
                sortedKey.Add(new KeyValuePair<int, char>(i, key[i]));
            }

            sortedKey.Sort(
                delegate (KeyValuePair<int, char> pair1, KeyValuePair<int, char> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                }
                );

            for (i = 0; i < keyLength; ++i)
                indexes[sortedKey[i].Key] = i;

        }

        public void code()
        {
            int messageLength = plainText.Length;
            int currentLetter = 0;

            while (currentLetter < messageLength)
            {
                char[] tab = new char[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    if(currentLetter < messageLength) { 
                     tab[i] = plainText[currentLetter];
                    currentLetter++;
                }
                    else
                        tab[i] = '#';
            }
                codeTable.Add(tab);
            }
        }
        public string GetCode()
        {
            var builder = new StringBuilder();
            int currentIndex;
            for (int i = 0; i < key.Length; i++)
            {
                currentIndex = Array.FindIndex(indexes, e => e == i);
                codeTable.ForEach(tab =>
                {
                    if (tab[currentIndex] != '#')
                        builder.Append(tab[currentIndex]);
               });
            }
            return builder.ToString();
        }
        public void decode()
        {
            int messageLength = plainText.Length;
            int currentLetter = 0;

            while (currentLetter < messageLength)
            {
                char[] tab = new char[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    if (currentLetter < messageLength)
                    {
                        tab[i] = '@';
                        currentLetter++;
                    }
                    else
                        tab[i] = '#';
                }
                codeTable.Add(tab);
            }

            currentLetter = 0;
            int currentIndex;

            for (int i = 0; i < key.Length; i++)
            {
                currentIndex = Array.FindIndex(indexes, e => e == i);
                codeTable.ForEach(tab =>
                {
                    if (tab[currentIndex] != '#' && currentLetter < plainText.Length)
                    {
                        tab[currentIndex] = plainText[currentLetter];
                        currentLetter++;
                    }
                    foreach (var it in tab)
                        Debug.Write(it);
                });
            }
        }
        public string GetDecode()
        {
            var builder = new StringBuilder();
            int currentIndex;

            codeTable.ForEach(tab =>
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    if (tab[i] != '#')
                        builder.Append(tab[i]);
                }
            });

            return builder.ToString();
        }
    }

}
