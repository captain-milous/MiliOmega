using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class Morseovka : Sifra
    {
        #region Konstruktory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public Morseovka(string text)
        { 
            RawText = text;
            Key = null;
            UnencryptedText = SimplifyRawText(RawText);
            EncryptedText = Encrypt(UnencryptedText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="deciphering"></param>
        public Morseovka(string text, bool deciphering) : base(text, deciphering) 
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #endregion
        public override string ToString()
        {
            return base.ToString();
        }
        #region Morseovka
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<char, string> morseCodeDict = new Dictionary<char, string>()
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
            {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
            {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
            {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"}, 
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
            {'Z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
            {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
            {'9', "----."}, {' ', "|"} // mezera
        };
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string Encrypt(string text)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in text.ToUpper())
            {
                if (morseCodeDict.ContainsKey(c))
                {
                    encryptedText.Append(morseCodeDict[c]);
                    encryptedText.Append("|"); // oddělovač mezi písmeny
                }
            }

            return encryptedText.ToString();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string Decrypt(string text)
        {
            StringBuilder decryptedText = new StringBuilder();

            string[] morseCodeWords = text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in morseCodeWords)
            {
                string[] morseCodeLetters = word.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string letter in morseCodeLetters)
                {
                    var morseCodeLetter = morseCodeDict.FirstOrDefault(x => x.Value == letter);

                    if (!string.IsNullOrEmpty(morseCodeLetter.Value))
                    {
                        decryptedText.Append(morseCodeLetter.Key);
                    }
                }

                decryptedText.Append(" "); // mezera mezi slovy
            }

            return decryptedText.ToString();
        }
        

    }
}
