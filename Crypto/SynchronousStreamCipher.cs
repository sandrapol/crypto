using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class SynchronousStreamCipher
    {
        private List<int> seed;
        private readonly string polyString;
        private readonly string fileName;
        private readonly string fileName2;

        public SynchronousStreamCipher(string fileName, string polyString, string seedString)
        {
            this.fileName = fileName;
            this.polyString = polyString;

            fileName2 = fileName.Replace(".txt", "2.txt");

            seed = new List<int>();

            foreach (char c in seedString)
            {
                seed.Add(c);
            }
        }

        // throws IOException
        public void Main()
        {
            List<int> toxor = new List<int>();
            List<int> xor = new List<int>();

            toxor = ReadFile(toxor, fileName);

            xor = Operations(toxor.Count);

            List<int> result = new List<int>();

            for (int i = 0; i < toxor.Count; i++)
            {
                if (toxor[i] == xor[i])
                {
                    result.Add(0);
                }
                else
                {
                    result.Add(1);
                }
            }


            List<string> letters = new List<string>();
            string a = "";

            for (int i = 0; i < result.Count; i++)
            {
                if (i % 8 != 0 || i == 0)
                {
                    a += result[i].ToString();
                }
                else
                {
                    letters.Add(a);
                    a = "";
                    a += result[i].ToString();
                }

                if (i == result.Count - 1)
                {
                    letters.Add(a);
                }
            }

            List<int> numbers = new List<int>();

            for (int i = 0; i < letters.Count; i++)
            {
                int number = Convert.ToInt32(letters[i], 2);
                numbers.Add(number);
            }

            SaveFile(numbers, fileName2);
        }

        public void Main2()
        {
            List<int> toxor = new List<int>();
            List<int> xor = new List<int>();


        }


        // throws FileNotFoundException, IOException
        public static List<int> ReadFile(List<int> toxor, string file)
        {
            StreamReader sr = new StreamReader(file);
            int _byte;
            List<int> list = new List<int>();

            do
            {
                _byte = sr.Read();
                list.Add(_byte);
            }
            while (_byte != -1);
            {
                list.Remove(list.Count - 1);
            }

            for (int i = 0; i < list.Count; i++)
            {
                string a = Convert.ToString(list[i], 2);

                if (a.Length < 8)
                {
                    string b = "";

                    for (int j = 0; j < 8 - a.Length; j++)
                    {
                        b += "0";
                    }

                    b += a;
                    a = b;
                }

                char[] letter = a.ToCharArray();

                for (int j = 0; j < 8; j++)
                {
                    if (letter[j] == '0')
                    {
                        toxor.Add(0);
                    }
                    else
                    {
                        toxor.Add(1);
                    }
                }
            }

            sr.Close();

            return toxor;
        }

        //  throws IOException
        public List<int> Operations(int listCount)
        {
            List<int> polynomial = new List<int>();
            string[] polyArray = polyString.Split(' ');

            foreach (string s in polyArray)
            {
                polynomial.Add(int.Parse(s));
            }

            /// seed 111001

            //// seed - from LFSR!!!! ////
            //List<int> seed = new List<int>();

            //for (int i = 0; i < polynomial[polynomial.Count - 1]; i++)
            //{
            //    seed.Add(0);
            //}

            ////

            List<int> result = new List<int>();
            int xor = 0;
            
            for (int i = 0; i < listCount; i++)
            {
                List<int> temp = new List<int>();

                for (int j = 0; j < polynomial.Count; j++)
                {
                    temp.Add(seed[polynomial[j] - 1]);
                }

                xor = XORfunc(temp);
                seed = Scroll(seed, xor);

                /// not used?
                //int number = To10(seed);
                result.Add(xor);
            }

            return result;
        }

        //throws FileNotFoundException, IOException
        //public static void SaveFile(List<int> result, string file)
        //{
        //    StreamWriter fos = new StreamWriter(file);
        //    int i = result.Count;
        //    int tmp = result.Count;
        //    byte[] dane_zaszyfrowane = new byte[tmp];

        //    for (int j = 0; j < tmp; j++)
        //    {
        //        dane_zaszyfrowane[j] = (byte)(int)result[j];
        //        fos.Write(dane_zaszyfrowane[j]);
        //    }

        //    //fos.Write(dane_zaszyfrowane);
        //    fos.Close();
        //}

        public void SaveFile(List<int> wynik, String nazwa)
        {
            FileStream fileOutput = new FileStream(nazwa, FileMode.Create, FileAccess.Write);
            int i = wynik.Count;
            int tmp = wynik.Count;
            byte[] dane_zaszyfrowane = new byte[tmp];
            for (int j = 0; j < tmp; j++)
            {

                dane_zaszyfrowane[j] = (byte)wynik[j];

            }
            fileOutput.Write(dane_zaszyfrowane, 0, dane_zaszyfrowane.Length);
            fileOutput.Close();
        }

    public static int XORfunc(List<int> list)
        {
            int xor = list[list.Count - 1];

            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (xor == list[i])
                {
                    xor = 0;
                }
                else
                {
                    xor = 1;
                }
            }

            return xor;
        }

        public static List<int> Scroll(List<int> list, int xor)
        {
            List<int> temp = new List<int>();
            temp.Add(xor);

            for (int i = 0; i < list.Count - 1; i++)
            {
                temp.Insert(i + 1, list[i]);
            }

            return temp;
        }

        public static int To10(List<int> list)
        {
            int number = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == 1)
                {
                    number += (int)Math.Pow(2, i);
                }
            }

            return number;
        }
    }
}
