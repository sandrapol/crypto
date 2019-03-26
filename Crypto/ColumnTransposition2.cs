using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class ColumnTransposition2
    {
        private List<char[]> codeTable;
        private string key;
        private string plainText;
        private string result;
        private int[] indexes;

        public ColumnTransposition2(string initial, string key)
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

            foreach(var it in indexes)
            {
                Debug.Write(it+ " ");
            }
          
        }

        public void code()
        {
            int messageLength = plainText.Length;
            int currentLetter = 0;
            int currentIndex =0;
            int currentLenght = Array.FindIndex(indexes, e => e == currentIndex);
            while (currentLetter < messageLength)
            {
                char[] tab = new char[key.Length];
                for(int i=0; i < key.Length; i++)
                {
                    if (i <= currentLenght)
                    {
                        tab[i] = plainText[currentLetter];
                        currentLetter++;
                    }
                    else
                        tab[i] = '#';
                }
                codeTable.Add(tab);
                currentIndex++;
                currentLenght = Array.FindIndex(indexes, e => e == currentIndex);
            }
        }
        public string GetCode()
        {
            var builder = new StringBuilder();
            int currentIndex;
            for(int i=0; i < key.Length; i++)
            {
                currentIndex= Array.FindIndex(indexes, e => e == i);
                codeTable.ForEach(tab =>
                {
                    if (tab[currentIndex] != '#')
                    {
                        builder.Append(tab[currentIndex]);
                    }
                });
            }
            return builder.ToString();
        }
        public void decode()
        {
            int messageLength = plainText.Length;
            int currentLetter = 0;
            int currentIndex = 0;
            int currentLenght = Array.FindIndex(indexes, e => e == currentIndex);
            while (currentLetter < messageLength)
            {
                char[] tab = new char[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    if (i <= currentLenght)
                    {
                        tab[i] ='@';
                        currentLetter++;
                    }
                    else
                        tab[i] = '#';
                }
                codeTable.Add(tab);
                currentIndex++;
                currentLenght = Array.FindIndex(indexes, e => e == currentIndex);
            }
            currentLetter = 0;
            for (int i = 0; i < key.Length; i++)
            {
                currentIndex = Array.FindIndex(indexes, e => e == i);
                codeTable.ForEach(tab =>
                {
                    if (tab[currentIndex] != '#' && currentLetter < plainText.Length)
                    {
                      tab[currentIndex]=plainText[currentLetter];
                        currentLetter++;   
                    }
                });
            }
        }
        public string GetDecode()
        {
            var builder = new StringBuilder();
            int currentIndex;

                codeTable.ForEach(tab =>
                {
                    for(int i=0;i<tab.Length; i++)
                    {
                        if (tab[i] != '#')
                            builder.Append(tab[i]);
                    }
                });

            return builder.ToString();
        }
    }
}
