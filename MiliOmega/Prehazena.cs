using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class Prehazena : Sifra
    {
        public enum ValidKey
        {
            REV,
            L1D,
            L1R
        }

        public override string Encrypt(string text, string key)
        {
            char[] encryptedText = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                int keyIndex = i % key.Length;
                char keyChar = key[keyIndex];
                int shift;

                if (keyChar == '1' && keyIndex % 2 == 0)
                    shift = i / 2;
                else if (keyChar == '1' && keyIndex % 2 == 1)
                    shift = (text.Length - 1) - (i / 2);
                else if (keyChar == 'R')
                    shift = -(key.Length - keyIndex);
                else
                    shift = keyChar - 'A';

                encryptedText[i] = ShiftCharacter(text[i], shift);
            }

            return new string(encryptedText);
        }

        public static string Decrypt(string text, string key)
        {
            char[] decryptedText = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                int keyIndex = i % key.Length;
                char keyChar = key[keyIndex];
                int shift;

                if (keyChar == '1' && keyIndex % 2 == 0)
                    shift = i / 2;
                else if (keyChar == '1' && keyIndex % 2 == 1)
                    shift = (text.Length - 1) - (i / 2);
                else if (keyChar == 'R')
                    shift = key.Length - keyIndex;
                else
                    shift = keyChar - 'A';

                decryptedText[i] = ShiftCharacter(text[i], -shift);
            }

            return new string(decryptedText);
        }

        private static char ShiftCharacter(char c, int shift)
        {
            if (!char.IsLetter(c))
                return c;

            char baseChar = char.IsUpper(c) ? 'A' : 'a';
            int alphabetSize = 26;
            return (char)(((c + shift - baseChar + alphabetSize) % alphabetSize) + baseChar);
        }
    }
}
