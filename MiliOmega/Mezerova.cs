using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class Mezerova : Sifra
    {
        public Mezerova(string text)
        {
            RawText = text;
            Key = null;
            UnencryptedText = SimplifyRawText(RawText);
            EncryptedText = Encrypt(UnencryptedText);
        }
        public Mezerova(string text, bool deciphering)
        {
            RawText = text;
            Key = null;

            if (!deciphering)
            {
                UnencryptedText = SimplifyRawText(RawText);
                EncryptedText = Encrypt(UnencryptedText);
            }
            else
            {
                EncryptedText = SimplifyRawText(RawText);
                UnencryptedText = Decrypt(EncryptedText);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string Encrypt(string text)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in text)
            {
                int index = Array.IndexOf(abeceda, c);
                if (index >= 0)
                {
                    char encryptedChar1 = abeceda[(index - 1 + abeceda.Length) % abeceda.Length];
                    char encryptedChar2 = abeceda[(index + 1) % abeceda.Length];
                    encryptedText.Append(encryptedChar1);
                    encryptedText.Append(encryptedChar2);
                }
                else
                {
                    if(c.ToString() != " ")
                    {
                        // Ponechat znaky, které nejsou písmena abecedy
                        encryptedText.Append(c);
                    }
                }
                if(c.ToString() != " ")
                {
                    encryptedText.Append(' ');
                }
            }

            return encryptedText.ToString();
        }

        public override string Decrypt(string text)
        {
            StringBuilder decryptedText = new StringBuilder();

            // Odstranění mezer mezi zašifrovanými znaky
            string[] encryptedChars = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < encryptedChars.Length; i += 2)
            {
                string encryptedChar1 = encryptedChars[i];
                string encryptedChar2 = encryptedChars[i + 1];

                int index1 = Array.IndexOf(abeceda, encryptedChar1[0]);
                int index2 = Array.IndexOf(abeceda, encryptedChar2[0]);

                if (index1 >= 0 && index2 >= 0)
                {
                    char decryptedChar = abeceda[(index2 + 1) % abeceda.Length];
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    // Ponechat znaky, které nejsou písmena abecedy
                    decryptedText.Append(encryptedChar1);
                    decryptedText.Append(encryptedChar2);
                }
            }

            return decryptedText.ToString();
        }

    }
}
