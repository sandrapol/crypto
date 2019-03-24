﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class JuliuszCezar
    {
        public char julek(char ch, int key)
        {
            if (!char.IsLetter(ch))
                return ch;

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }

        public string Encipher (string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += julek(ch, key);

            return output;
        }

        public string Decipher (string input, int key)
        {
            return Encipher(input, 26 - key);
        }
       
    }
}
