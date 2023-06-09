﻿using System;
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
            UnencryptedText = SimplifyRawText(RawText);
            EncryptedText = Encrypt(UnencryptedText);
        }
        public CislaMistoPismen(string text, bool deciphering) 
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected int ChangeToNum(string input)
        {
            input = input.ToUpper();

            for (int i = 0; i < abeceda.Length; i++)
            {
                if (abeceda[i].ToString() == input)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        protected string ChangeToChar(int input)
        {
            if (input > 0 && input <= abeceda.Length)
            {
                input--;
                return abeceda[input].ToString();
            }

            return string.Empty;
        }

    }
}
