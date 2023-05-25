using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class CislaMistoPismen : Sifra
    {

        public CislaMistoPismen(string text)
        {
            RawText = text;
            Key = null;
            UnencryptedText = GetRidOfDiacriticsAndSmallLetters(RawText);
            EncryptedText = Encrypt(UnencryptedText);
        }
        public CislaMistoPismen(string text, bool deciphering) 
        {
            RawText = text;
            Key = null;

            if (!deciphering)
            {
                UnencryptedText = GetRidOfDiacriticsAndSmallLetters(RawText);
                EncryptedText = Encrypt(UnencryptedText);
            }
            else
            {
                EncryptedText = GetRidOfDiacriticsAndSmallLetters(RawText);
                UnencryptedText = Decrypt(EncryptedText);
            }
        }


        public override string ToString()
        {
            return base.ToString();
        }

        public override string Encrypt(string text)
        {
            List<string> encryptedList = new List<string>();
            foreach (char c in text)
            {
                if (abeceda.Contains(c))
                {
                    int znak = ChangeToNum(c.ToString());
                    encryptedList.Add(znak.ToString());
                } 
                else if (c.ToString() == " ")
                {
                    // adds nothing
                }
                else
                {
                    encryptedList.Add(c.ToString());
                }
            }

            string encryptedText = String.Join(" ", encryptedList);
            return encryptedText;
        }

        public override string Decrypt(string text)
        {
            List<string> encryptedList = text.Split(" ").ToList();
            StringBuilder decryptedText = new StringBuilder();

            foreach (string encryptedChar in encryptedList)
            {
                if (int.TryParse(encryptedChar, out int index))
                {
                    if (index >= 1 && index <= abeceda.Length)
                    {
                        char decryptedChar = abeceda[index - 1];
                        decryptedText.Append(decryptedChar);
                    }
                    else
                    {
                        // Handle invalid index (out of range)
                        decryptedText.Append("?");
                    }
                }
                else
                {
                    // Handle non-numeric characters (special characters)
                    decryptedText.Append(encryptedChar);
                }
            }

            return decryptedText.ToString();
        }

        

        
    }
}
