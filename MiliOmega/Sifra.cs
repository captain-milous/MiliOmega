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

        protected enum ValidKey
        {

        }

        /// <summary>
        /// Text co se zadá úplně poprvé, vůbec se s ním nadále nepracuje.
        /// </summary>
        protected string RawText { get { return rawText; } set { rawText = value; } }
        /// <summary>
        /// Text, který je nezašifrovaný
        /// </summary>
        protected string UnencryptedText 
        { 
            get { return unencryptedText; } 
            set 
            { 
                unencryptedText = SimplifyRawText(value);  
            } 
        }
        /// <summary>
        /// Text, který je zašifrovaný
        /// </summary>
        protected string EncryptedText 
        { 
            get { return encryptedText; } 
            set 
            { 
                encryptedText = value; 
            } 
        }
        /// <summary>
        /// Klíč k šifrování (Není nutný u všech šifer -> může být null)
        /// </summary>
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
        /// <summary>
        /// Prázdný konstruktor
        /// </summary>
        protected Sifra()
        {
            RawText = "";
            UnencryptedText = string.Empty;
            EncryptedText = string.Empty;
            Key = null;
        }
        /// <summary>
        /// Konstruktor bez klíče, ve kterém rozhodnete jestli chcete šifrovat zadaný text nebo ho rozšifrovat.
        /// </summary>
        /// <param name="text">Na zašifrování či rozšifrování</param>
        /// <param name="deciphering">true -> rozšifrování; false -> zašifrování</param>
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
        /// <summary>
        /// Konstruktor s klíče, ve kterém rozhodnete jestli chcete šifrovat zadaný text nebo ho rozšifrovat.
        /// </summary>
        /// <param name="text">Na zašifrování či rozšifrování</param>
        /// <param name="key">Klíč k šifrování textu</param>
        /// <param name="deciphering">true -> rozšifrování; false -> zašifrování</param>
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
        /// <summary>
        /// Veřejná metoda pro vrácení vloženého textu
        /// </summary>
        /// <returns></returns>
        public string GetRawText() { return rawText; }
        public string GetUnencryptedText() { return unencryptedText; }
        public string GetEncryptedText() { return encryptedText; }
        public string? GetKey() { return key; }
        /// <summary>
        /// Výpis Sifer
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Zašifrování bez klíče
        /// </summary>
        /// <param name="text">text který se šifruje</param>
        /// <returns>zašifrovaný text</returns>
        public virtual string Encrypt(string text)
        {
            return text;
        }
        /// <summary>
        /// Zašifrování textu s klíčem (pokud je klíč null nebo jen mezera tak to vyvolá metodu Zašifrování bez klíče)
        /// </summary>
        /// <param name="text">text který se šifruje</param>
        /// <param name="key">klíč kterým se šifruje</param>
        /// <returns>zašifrovaný text</returns>
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
        /// Rozšifrování textu bez klíče
        /// </summary>
        /// <param name="text">text který se má rozšifrovat</param>
        /// <returns>rozšifrovaný text</returns>
        public virtual string Decrypt(string text)
        {
            return text;
        }
        /// <summary>
        /// Rozšifrování textu s klíčem (pokud je klíč null nebo jen mezera tak to vyvolá metodu Rozšifrování bez klíče)
        /// </summary> 
        /// <param name="text">text který se má rozšifrovat</param>
        /// <param name="key">klíč kterým se rozšifruje</param>
        /// <returns>rozšifrovaný text</returns>
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
        /// Udělá z textu, text který je bez diakritiky a zbaví se malých písmen
        /// </summary>
        /// <param name="input">Prostý text</param>
        /// <returns>Jednodušší text</returns>
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
        /// <summary>
        /// ABECEDA
        /// </summary>
        protected char[] abeceda = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        /// <summary>
        /// Najde index písmenka v abecedě pokud není v abecedě tak vrátí -1
        /// </summary>
        /// <param name="input">Písmeno v abecedě</param>
        /// <returns>Index písmena</returns>
        protected int FindIndexInAlphabet(string input)
        {
            input = input.ToUpper();
            for (int i = 0; i < abeceda.Length; i++)
            {
                if (abeceda[i].ToString() == input)
                {
                    return i;
                }
            }
            return -1;
        }
        

    }
}
