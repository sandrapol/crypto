using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Stream3
    {
        private List<int> polynomial = new List<int>();
        private List<int> seedList= new List<int>();
        private String source;
        private String target;
        private List<int> input = new List<int>();

        Stream3(String key,String seed, String source, String target)
        {
            System.out.println("Podaj potegi wielomianu (wpisz end aby zakonczyc) np. 2 3 4 = x^2+x^3+x^4");
            keyToList(key);
            int ile = polynomial.get(polynomial.size() - 1);
            System.out.println("Podaj " + ile + " wartosci 0 lub 1:");
            seedToList(seed);
            System.out.println("podaj co odczytac");
            this.source = source;

            readFromFile(input, source);
            System.out.println("podaj gdzie zapisac");
            target = this.target;
        }

        private void keyToList(String key)
        {
            int control = 1;
            String[] charArr = key.Split(' ');
            for (int i = 0; i < charArr.Length; i++)
            {
                if (Int32.TryParse(charArr[i], out control))
                    this.polynomial.Add(Int32.Parse(charArr[i]));
                else
                    throw new System.ArgumentException("Key needs to be x x x x (x-numbers)", "Key");
            }
        }

        private void seedToList(String seed)
        {
            int control = 1;
            char[] charArr = seed.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                if (!Int32.TryParse(charArr[i].ToString(), out control))
                    throw new System.ArgumentException("Values needs to be xxxx (x-numbers)", "Values");
                if (Int32.Parse(charArr[i].ToString()) != 0 || Int32.Parse(charArr[i].ToString()) != 1)
                    throw new System.ArgumentException("Values needs to be 1 or 0", "Values");
                this.seedList.Add(Int32.Parse(charArr[i].ToString()));
            }
        }

        public void Code()
        {
            List<int> wynik = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                int co = input[i];
                wynik.Add(rejestr(co));
            }

            List<String> litery = new List<String>();
            String a = "";


            for (int i = 0; i < wynik.Count; i++)
            {
                if (i % 8 != 0 || i == 0)
                {

                    a += wynik[i].ToString();
                }
                else
                {
                    litery.Add(a);
                    a = "";
                    a += wynik[i].ToString();
                }
                if (i == wynik.Count - 1)
                {
                    litery.Add(a);
                }
            }
            List<int> numbers = new List<int>();
            for (int i = 0; i < litery.Count; i++)
            {
                int number = Int32.Parse(litery[i]);
                //     System.out.println(number);
                numbers.Add(number);
            }
            writeToFile(numbers, target);
        }
        public void Decode()
        {

            List<int> wynik = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                int co = input[i];
                wynik.Add(rejestr2(co));
            }

            List<String> litery = new List<String>();
            String a = "";

            for (int i = 0; i < wynik.Count; i++)
            {
                if (i % 8 != 0 || i == 0)
                {
                    a += wynik[i].ToString();
                }
                else
                {
                    litery.Add(a);
                    a = "";
                    a += wynik[i].ToString();
                }
                if (i == wynik.Count - 1)
                {
                    litery.Add(a);
                }
            }
            List<int> numbers = new List<int>();
            for (int i = 0; i < litery.Count; i++)
            {
                int number = Int32.Parse(litery[i]);
                numbers.Add(number);
            }
            writeToFile(numbers, target);
        }

        public int rejestr2(int co)
        {
            int randomize = 1;
            int xor = 0;
            int t = 0;
            for (int i = 0; i < randomize; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < polynomial.Count; j++)
                {
                    temp.Add(seedList[polynomial[j - 1]]);
                }

                xor = xorM(temp);
                if (xor == co)
                {
                    t = 0;
                }
                else
                {
                    t = 1;
                }
                seedList = przesun(seedList, co);
                int number = to10(seedList);


            }
            return t;
        }

        public int rejestr(int co)
        {
            int randomize = 1;
            int xor = 0;
            int t = 0;
            for (int i = 0; i < randomize; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < polynomial.Count; j++)
                {
                    temp.Add(seedList[polynomial[j - 1]]);
                }

                xor = xorM(temp);
                if (xor == co)
                {
                    t = 0;
                }
                else
                {
                    t = 1;
                }
                seedList = przesun(seedList, t);
                int number = to10(seedList);


            }
            return t;
        }

        public static int to10(List<int> list)
        {
            int number = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == 1)
                {
                    number += Convert.ToInt32(Math.Pow(2, i));
                }
            }
            return number;
        }

        public static int xorM(List<int> list)
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

        public List<int> przesun(List<int> list, int xor)
        {
            List<int> temp = new List<int>();
            temp.Add(xor);
            for (int i = 0; i < list.Count - 1; i++)
            {
                temp.Insert(i + 1, list[i]);
            }
            return temp;
        }
        public List<int> readFromFile(List<int> toxor, String nazwa)
        {
            StreamReader fileInput = new StreamReader(nazwa);
            int currentByte;
            List<int> byteList = new List<int>();
            do
            {
                currentByte = fileInput.Read();
                byteList.Add(currentByte);
            } while (currentByte != -1);
            byteList.Remove(byteList.Count - 1);
            for (int i = 0; i < byteList.Count; i++)
            {
                String a = Convert.ToString(byteList[i],2);
                if (a.Length < 8)
                {
                    String b = "";
                    for (int j = 0; j < 8 - a.Length; j++)
                    {
                        b += "0";
                    }
                    b += a;
                    a = b;
                }
                char[] litera = a.ToCharArray();
                for (int j = 0; j < 8; j++)
                {
                    if (litera[j] == '0')
                    {
                        toxor.Add(0);
                    }
                    else
                    {
                        toxor.Add(1);
                    }
                }
            }
            fileInput.Close();
            return toxor;
        }
        public void writeToFile(List<int> wynik, String nazwa)
        {
            StreamWriter fileOutput = new StreamWriter(nazwa);
            int i = wynik.Count;
            int tmp = wynik.Count;
            byte[] dane_zaszyfrowane = new byte[tmp];
            for (int j = 0; j < tmp; j++)
            {

                dane_zaszyfrowane[j] = (byte)(int)wynik[j];
            }
            fileOutput.Write(dane_zaszyfrowane);
            fileOutput.Close();
        }
    }
}
