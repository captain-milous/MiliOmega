using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class CislaMistoPismen : Sifra
    {

        public CislaMistoPismen(string text) : base(text, false) 
        {
            RawText = text;
            Key = null;
            UnencryptedText = GetRidOfDiacriticsAndSmallLetters(RawText);
            EncryptedText = Encrypt(UnencryptedText);
        }
        public CislaMistoPismen(string text, bool deciphering) : base(text, deciphering) 
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
                else
                {
                    encryptedList.Add(c.ToString());
                }
            }

            string encryptedText = String.Join("", encryptedList) + "||";
            return encryptedText;
        }

        public override string Decrypt(string text)
        {
            //dodělat
            return "";
        }

        public int ChangeToNum(string input)
        {
            // Convert the input to uppercase to ensure case insensitivity
            input = input.ToUpper();

            for (int i = 0; i < abeceda.Length; i++)
            {
                if (abeceda[i].ToString() == input)
                {
                    return i + 1;
                }
            }

            // If the input is not found in the alphabet, return -1 (or any other suitable value)
            return -1;
        }

        public string ChangeToChar(int input)
        {
            if(input > 0 && input <= abeceda.Length)
            {
                input--;
                return abeceda[input].ToString();
            }

            return string.Empty;
        }
    }
}
