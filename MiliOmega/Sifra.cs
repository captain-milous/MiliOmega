using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace MiliOmega
{
    public class Sifra
    {
        #region Proměnné
        protected string rawText;
        protected string unencryptedText;
        protected string encryptedText;
        protected string? key;

        /// <summary>
        /// 
        /// </summary>
        protected string RawText { get { return rawText; } set { rawText = value; } }
        
        protected string UnencryptedText 
        { 
            get { return unencryptedText; } 
            set 
            { 
                unencryptedText = SimplifyRawText(value);  
            } 
        }
        
        protected string EncryptedText 
        { 
            get { return encryptedText; } 
            set 
            { 
                encryptedText = value; 
            } 
        }
        
        protected string? Key 
        { 
            get { return key; }
            set 
            { 
                key = value;
            } 
        }
        #endregion
        #region Konstruktory
        protected Sifra()
        {
            RawText = "";
            UnencryptedText = string.Empty;
            EncryptedText = string.Empty;
            Key = null;
        }

        protected Sifra(string text, bool deciphering)
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
                EncryptedText = text;
                UnencryptedText = Decrypt(EncryptedText);
            }
        }
        protected Sifra(string text, string key, bool deciphering)
        {
            RawText = text;
            Key = key;

            if (!deciphering)
            {
                UnencryptedText = SimplifyRawText(RawText);
                EncryptedText = Encrypt(UnencryptedText, Key);
            }
            else
            {
                EncryptedText = text;
                UnencryptedText = Decrypt(EncryptedText, Key);                
            }
        }
        #endregion

        public string GetRawText() { return rawText; }
        public string GetUnencryptedText() { return unencryptedText; }
        public string GetEncryptedText() { return encryptedText; }
        public string? GetKey() { return key; }

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
        #region Zašifrování
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
        #endregion
        #region Rozšifrování
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual string Decrypt(string text)
        {
            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string Decrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Decrypt(text);
            }

            return text;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SimplifyRawText(string input)
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

        protected char[] abeceda = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        protected int FindIndexInAlphabet(string input)
        {
            // Convert the input to uppercase to ensure case insensitivity
            input = input.ToUpper();

            for (int i = 0; i < abeceda.Length; i++)
            {
                if (abeceda[i].ToString() == input)
                {
                    return i;
                }
            }

            // If the input is not found in the alphabet, return -1 (or any other suitable value)
            return -1;
        }

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
