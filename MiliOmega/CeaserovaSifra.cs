using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class CeaserovaSifra : Sifra
    {
        private List<char> ABC = new List<char>() 
        { 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' 
        };
        private Dictionary<string, char> posunutaABC = new Dictionary<string, char>();

        public CeaserovaSifra(string text, string key, bool deciphering) : base(text, deciphering)
        {
            if(key.Length == 1)
            {
                Key = key.ToUpper();
            }
        }

        public virtual string Encrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key == "A")
            {
                return Encrypt(text);
            }

            return text;
        }

        public virtual string Decrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key == "A")
            {
                return Decrypt(text);
            }

            return text;
        }

    }
}
