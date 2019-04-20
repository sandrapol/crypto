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
        private readonly string LFSRdegree;
        private readonly string fileName;
        private readonly string fileName2;

        public SynchronousStreamCipher(string LFSRdegree, string fileName)
        {
            this.fileName = fileName;
            this.LFSRdegree = LFSRdegree;

            fileName2 = fileName.Replace(".txt", "2.txt");
        }

        public void Main()
        {
            List<int> fileBinary = new List<int>();
            List<int> lfsrBinary = new List<int>();

            fileBinary = ReadFile(fileBinary, fileName);
            int fileLength = fileBinary.Count;

            LFSR lfsr = new LFSR();
            string blank = "";
            string lfsrOutput = lfsr.Encrypt(LFSRdegree, fileLength, ref blank);

            foreach(char c in lfsrOutput)
            {
                lfsrBinary.Add(int.Parse(c.ToString()));
            }

            List<int> result = new List<int>();

            for (int i = 0; i < fileBinary.Count; i++)
            {
                if (fileBinary[i] == lfsrBinary[i])
                {
                    result.Add(0);
                }
                else
                {
                    result.Add(1);
                }
            }

            SaveFile(result, fileName2);
        }

        public static List<int> ReadFile(List<int> fileBinary, string file)
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
                        fileBinary.Add(0);
                    }
                    else
                    {
                        fileBinary.Add(1);
                    }
                }
            }

            sr.Close();

            return fileBinary;
        }

        public void SaveFile(List<int> result, string fileName)
        {
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

            FileStream fileOutput = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            int count = numbers.Count;
            byte[] encryptedData = new byte[count];

            for (int i = 0; i < count; i++)
            {
                encryptedData[i] = (byte)numbers[i];
            }

            fileOutput.Write(encryptedData, 0, encryptedData.Length);
            fileOutput.Close();
        }
    }
}
