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
        public Morseovka() : base(){ }
        public Morseovka(string text) : base(text, false) { }
        public Morseovka(string text, bool deciphering) : base(text, deciphering) { }
        public Morseovka(string text, string key, bool deciphering) : base(text, deciphering)
        {
            Key = null;
        }
        #endregion
        public override string ToString()
        {
            return base.ToString();
        }
        #region Morseovka
        private Dictionary<char, string> morseCodeDictionary = new Dictionary<char, string>()
            {
                {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
                {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
                {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
                {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
                {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
                {'Z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
                {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
                {'9', "----."}, {' ', ""}, {'.', ""}, {'?', ""}, {'!', ""}
            };
        private Dictionary<string, char> morseCodeDictionaryRev = new Dictionary<string, char>()
            {
                {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'}, {".", 'E'},
                {"..-.", 'F'}, {"--.", 'G'}, {"....", 'H'}, {"..", 'I'}, {".---", 'J'},
                {"-.-", 'K'}, {".-..", 'L'}, {"--", 'M'}, {"-.", 'N'}, {"---", 'O'},
                {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'}, {"...", 'S'}, {"-", 'T'},
                {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'}, {"-.--", 'Y'},
                {"--..", 'Z'}, {"-----", '0'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'},
                {"....-", '4'}, {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'},
                {"----.", '9'}, {"|", ' '}
            };
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string Encrypt(string text)
        {
            List<string> morseCodeList = new List<string>();
            foreach (char c in text)
            {
                if (morseCodeDictionary.ContainsKey(c))
                {
                    morseCodeList.Add(morseCodeDictionary[c]);
                }
            }

            string encryptedText = String.Join("|", morseCodeList) + "||";
            return encryptedText;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string Decrypt(string text)
        {
            List<string> morseCodeList = text.Split('|').ToList();
            string decryptedText = "";
            foreach (string morseCode in morseCodeList)
            {
                if (morseCodeDictionaryRev.ContainsKey(morseCode))
                {
                    decryptedText += morseCodeDictionaryRev[morseCode];
                }
            }
            return decryptedText;
        }
    
    }
}
