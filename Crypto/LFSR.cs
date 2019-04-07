using System;
using System.Security.Cryptography;

namespace Crypto
{
    class LFSR
    {
        private bool[] _lfsr = null;
        private bool[] _feedbackPoints = new bool[265];
        private int _lfsrLength = 0;

        public LFSR (int length, int [] feedbackPoints) : this (length, feedbackPoints, new byte[0]) { }

        public LFSR (int length, int [] feedbackPoints, byte[] seed)
        {
            if (length > 256)
                throw new ArgumentOutOfRangeException("length", "Wartości powinny być z przedziału 1-256");

            _lfsrLength = length;
            _lfsr = new bool[length];

            foreach (int feedbackPoint in feedbackPoints)
                if (feedbackPoint > 256)
                    throw new ArgumentOutOfRangeException("feedbackPoint", "Dozwolona wartość elementu tablicy jest z przedziału 1-256");
                else
                    _feedbackPoints[feedbackPoint] = true;


            byte[] randomizedSeed = this.SeedRandomization(seed);

            string temporaryRegisterRepresentation = string.Empty;
            foreach (byte seedItem in randomizedSeed)
                temporaryRegisterRepresentation += Convert.ToString(seedItem, 2);

            int index = 0;
            foreach (char bit in temporaryRegisterRepresentation)
                if (index < length)
                    _lfsr[index++] = bit == '1';
        }

        public bool Clock()
        {
            lock (this)
            {
                bool output = _lfsr[0];

                for (int index = 0; index < _lfsrLength - 1; index++)
                    _lfsr[index] = _feedbackPoints[index] ? _lfsr[index + 1] ^ output : _lfsr[index + 1];

                _lfsr[_lfsrLength - 1] = output;

                return output;
            }
        }

        private byte[] SeedRandomization(byte[] inputSeed)
        {
            SHA256Managed sHA256 = new SHA256Managed();
            int seedLength = inputSeed.Length;

            byte[] seed = new byte[seedLength];
            Array.Copy(inputSeed, seed, seedLength);

            Array.Resize<byte>(ref seed, seedLength + 4);
            byte[] dateTime = BitConverter.GetBytes(DateTime.Now.Ticks);
            seed[seedLength] = dateTime[0];
            seed[seedLength+1] = dateTime[1];
            seed[seedLength+2] = dateTime[2];
            seed[seedLength+3] = dateTime[3];

            return sHA256.ComputeHash(seed, 0, seed.Length);

        }
    }
}
