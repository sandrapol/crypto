using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Cipher
    {
        private char[] plainText;
        private string result;
        private int[] key;
        private List<char[]> codeTable;

        public Cipher(string initial, string key) 
        {
            this.plainText = initial.Replace(":", "").Replace(".", "").Replace(",", "").Replace(" ","").ToCharArray();
            this.result = "";
            try
            {
                keyToArray(key);
                validateKey();
            }catch(ArgumentException error)
            {
                throw error;
            }
        }

        public string getResult()
        {
            var builder = new StringBuilder();
            foreach(var line in codeTable)
            {
                for(int i=0;i<key.Length;i++)
                    if(!Char.IsControl(line[key[i]-1]))
                    builder.Append(line[key[i] - 1]);

            }
            result = builder.ToString();

            return result;

        }
        public string getDecodeResult()
        {
            var builder = new StringBuilder();
            foreach (var line in codeTable)
            {
                for (int i = 0; i < key.Length; i++)
                    if (!Char.IsControl(line[i]))
                        builder.Append(line[i]);

            }
            result = builder.ToString();

            return result;

        }



        public void code()
        {
            this.codeTable = new List<char[]>();
            char[] line = new char[key.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                line[i%key.Length] = plainText[i];
                if (i % key.Length == (key.Length - 1) || i == plainText.Length-1)
                {
                    codeTable.Add(line);
                    line = new char[key.Length];
                }      
            }
            Debug.WriteLine(codeTable);
            foreach (var item in codeTable)
            {
                Debug.WriteLine(item);
                foreach (var li in item)
                    Debug.Write(li+" ");
                Debug.WriteLine(" ");
            }
        }
        public void decode()
        {
            this.codeTable = new List<char[]>();
            char[] line = new char[key.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                line[i % key.Length] = '#';
                if (i % key.Length == (key.Length - 1) || i == plainText.Length - 1)
                {
                    codeTable.Add(line);
                    line = new char[key.Length];
                }
            }
            int currentLetter = 0;
            codeTable.ForEach(tab =>
            {
                for(int i = 0; i < tab.Length; i++)
                {
                    Debug.WriteLine("klucz"+key[i]);
                    Debug.WriteLine("linia"+tab[key[i] - 1]);
                    Debug.WriteLine("Tekst"+plainText[currentLetter]);
                    if (tab[key[i] - 1]=='#' && currentLetter<plainText.Length)
                    {
                      
                        tab[key[i] - 1] = plainText[currentLetter];
                        currentLetter++;
                    }
                }
            });
           
            Debug.WriteLine(codeTable);
            foreach (var item in codeTable)
            {
                Debug.WriteLine(item);
                foreach (var li in item)
                    Debug.Write(li + " ");
                Debug.WriteLine(" ");
            }
        }

        private bool validateKey()
        {
            for (int i = 1; i <= key.Length; i++)
            {
                if (!key.Contains(i))
                    throw new System.ArgumentException("Key need to have "+i+" too", "Key");
            }
            return true;
        }

        private void keyToArray(string key)
        {
            int control = 1;
            string[] charArr = key.Split('-');
            this.key = new int[charArr.Length];
            for (int i = 0; i < charArr.Length; i++)
            {
                if (Int32.TryParse(charArr[i], out control))
                     this.key[i] = Int32.Parse(charArr[i]);
                else
                    throw new System.ArgumentException("Key needs to be x-x-x-x-... (x-number)", "Key");
            }

        }
    }
}
