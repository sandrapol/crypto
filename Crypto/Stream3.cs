using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Stream3
    {
        private List<int> polynomial = new List<int>();
        private List<int> seedList = new List<int>();
        private String source;
        private String target;
        private String keyFile;
        private List<int> input = new List<int>();

        public Stream3(String key, String seed, String source)
        {
            keyToList(key);
            int ile = polynomial[polynomial.Count - 1];
            seedToList(seed);
            this.source = source;
            readFromFile();
            target = source.Replace(".txt", "2.txt");
            keyFile = source.Replace(".txt", "Key.txt");
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
                if (Int32.Parse(charArr[i].ToString()) != 0 && Int32.Parse(charArr[i].ToString()) != 1)
                    throw new System.ArgumentException("Values needs to be 1 or 0", "Values");
                this.seedList.Add(Int32.Parse(charArr[i].ToString()));
            }
        }

        public List<int> Code()
        {
            List<int> codedBits = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                codedBits.Add(getCodedBit(input[i]));
            }

            List<String> byteList = new List<String>();
            String currentByte = "";

            for (int i = 0; i < codedBits.Count; i++)
            {
                if (i % 8 != 0 || i == 0)
                {

                    currentByte += codedBits[i].ToString();
                }
                else
                {
                    byteList.Add(currentByte);
                    currentByte = "";
                    currentByte += codedBits[i].ToString();
                }
                if (i == codedBits.Count - 1)
                {
                    byteList.Add(currentByte);
                }
            }

            List<int> result = new List<int>();
            for (int i = 0; i < byteList.Count; i++)
            {
                result.Add(Convert.ToInt32(byteList[i], 2));
            }
            writeToFile(result, target);
            writeKeyToFile();
            return seedList;
        }
        public  string getDestination()
        {
            return "Zakodowany tekst znajduje się w " + target;
        }

        public string Decode()
        {

            List<int> codedBits = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                codedBits.Add(getDecodedBit(input[i]));
            }

            List<String> byteList = new List<String>();
            String currentByte = "";

            for (int i = 0; i < codedBits.Count; i++)
            {
                if (i % 8 != 0 || i == 0)
                {
                    currentByte += codedBits[i].ToString();
                }
                else
                {
                    byteList.Add(currentByte);
                    //currentByte = "";
                    currentByte = codedBits[i].ToString();
                }
                if (i == codedBits.Count - 1)
                {
                    byteList.Add(currentByte);
                }
            }

            List<int> result = new List<int>();
            for (int i = 0; i < byteList.Count; i++)
            {
                result.Add(Convert.ToInt32(byteList[i], 2));
            }
            writeToFile(result, target);
            return "Odkodowany tekst znajduje się w pliku " + target;
        }

        public int getDecodedBit(int co)
        {
            int randomize = 1;
            int xor = 0;
            int decryptedBit = 0;
            for (int i = 0; i < randomize; i++)
            {
                List<int> bitToXor = new List<int>();
                for (int j = 0; j < polynomial.Count; j++)
                {
                    bitToXor.Add(seedList[polynomial[j] - 1]);
                }

                xor = doXOR(bitToXor);
                if (xor == co)
                {
                    decryptedBit = 0;
                }
                else
                {
                    decryptedBit = 1;
                }
                seedList = moveRight(seedList, co);
            }
            return decryptedBit;
        }

        public int getCodedBit(int currentBit)
        {
            int randomize = 1;
            int xor = 0;
            int encryptedBit = 0;
            for (int i = 0; i < randomize; i++)
            {
                List<int> bitesToXor = new List<int>();
                for (int j = 0; j < polynomial.Count; j++)
                {
                    bitesToXor.Add(seedList[polynomial[j] - 1]);
                }

                xor = doXOR(bitesToXor);
                if (xor == currentBit)
                {
                    encryptedBit = 0;
                }
                else
                {
                    encryptedBit = 1;
                }
                seedList = moveRight(seedList, encryptedBit);
            }
            return encryptedBit;
        }

        public static int doXOR(List<int> list)
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

        public List<int> moveRight(List<int> list, int xor)
        {
            List<int> temp = new List<int>();
            temp.Add(xor);
            for (int i = 0; i < list.Count - 1; i++)
            {
                temp.Insert(i + 1, list[i]);
            }
            return temp;
        }

        public void readFromFile()
        {
            FileStream fileInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            byte[] inputBytes = new byte[fileInput.Length];
            List<int> byteList = new List<int>();

            fileInput.Read(inputBytes, 0, (int)fileInput.Length);

            foreach (var elem in inputBytes)
            {
                byteList.Add(elem);
            }
            byteList.Remove(byteList.Count - 1);
            for (int i = 0; i < byteList.Count; i++)
            {
                String currentByte = Convert.ToString(byteList[i], 2);
                if (currentByte.Length < 8)
                {
                    String helper = "";
                    for (int j = 0; j < 8 - currentByte.Length; j++)
                    {
                        helper += "0";
                    }
                    helper += currentByte;
                    currentByte = helper;
                }
                char[] currentByteArr = currentByte.ToCharArray();
                for (int j = 0; j < 8; j++)
                {
                    if (currentByteArr[j] == '0')
                    {
                        input.Add(0);
                    }
                    else
                    {
                        input.Add(1);
                    }
                }
            }
            fileInput.Close();
        }
        public void writeToFile(List<int> result, String fileName)
        {
            FileStream fileOutput = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            int i = result.Count;
            int tmp = result.Count;
            byte[] byteResult = new byte[tmp];
            for (int j = 0; j < tmp; j++)
            {

                byteResult[j] = (byte)result[j];

            }
            fileOutput.Write(byteResult, 0, byteResult.Length);
            fileOutput.Close();
        }

        public void writeKeyToFile()
        {
            FileStream fileOutput = new FileStream(keyFile, FileMode.OpenOrCreate, FileAccess.Write);
            int i = seedList.Count;
            int tmp = seedList.Count;
            byte[] output = new byte[tmp];
            for (int j = 0; j < tmp; j++)
            {

                output[j] = (byte)seedList[j];

            }
            fileOutput.Write(output, 0, seedList.Count);
            fileOutput.Close();
        }
    }
}
