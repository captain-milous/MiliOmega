using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MiliOmega
{
    public class Sifra
    {
        protected string rawText;
        protected string unencryptedText;
        protected string encryptedText;
        protected string? key;

        public string RawText { get { return rawText; } set { rawText = value; } }
        public string UnencryptedText 
        { 
            get { return unencryptedText; } 
            set 
            { 
                unencryptedText = GetRidOfDiacriticsAndSmallLetters(value);  
                encryptedText = Encrypt(unencryptedText);  
            } 
        }
        public string EncryptedText 
        { 
            get { return encryptedText; } 
            set 
            { 
                encryptedText = value; 
                unencryptedText = Decrypt(encryptedText);  
            } 
        }
        public string? Key 
        { 
            get { return key; }
            set 
            { 
                key = value;
                Encrypt(unencryptedText, value);
            } 
        }

        public Sifra()
        {
            RawText = "";
            UnencryptedText = string.Empty;
            EncryptedText = string.Empty;
            Key = null;
        }

        public Sifra(string text, bool deciphering)
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
                EncryptedText = text;
                UnencryptedText = Decrypt(EncryptedText);
            }
        }
        public Sifra(string text, string key, bool deciphering)
        {
            RawText = text;
            Key = key;

            if (!deciphering)
            {
                UnencryptedText = GetRidOfDiacriticsAndSmallLetters(RawText);
                EncryptedText = Encrypt(UnencryptedText, Key);
            }
            else
            {
                EncryptedText = text;
                UnencryptedText = Decrypt(EncryptedText, Key);                
            }
        }

        public override string ToString()
        {
            if(key == null)
            {
                return UnencryptedText + " = " + EncryptedText;
            } 
            else
            {
                return UnencryptedText + " with key " + Key + " = " + EncryptedText;
            }
            
        }

        public virtual string Encrypt(string text)
        {
            return text;
        }

        public virtual string Encrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Encrypt(text);
            }

            return text;
        }

        public virtual string Decrypt(string text)
        {
            return text;
        }
        public virtual string Decrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Decrypt(text);
            }

            return text;
        }

        public string GetRidOfDiacriticsAndSmallLetters(string input)
        {
            string output = "";
            if (string.IsNullOrWhiteSpace(input))
            {
                return output;
            }

            input = input.ToUpper();
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(normalizedString[i]);
                }
            }

            output = stringBuilder.ToString();
            return output;
        }

    }
}
