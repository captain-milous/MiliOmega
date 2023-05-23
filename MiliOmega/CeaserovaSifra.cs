using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class CeaserovaSifra : Sifra
    {
        private char[] abeceda = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public CeaserovaSifra(string text, string key) : base(text, false)
        {
            key.Replace(" ", "");
            if (key.Length == 3)
            {
                Key = key.ToUpper();
            }
            else
            {
                Key = "A=A";
            }
            EncryptedText = Encrypt(UnencryptedText, Key);
        }

        public CeaserovaSifra(string text, string key, bool deciphering) : base(text, deciphering)
        {
            key.Replace(" ", "");
            if (key.Length == 3)
            {
                Key = key.ToUpper();
            } 
            else
            {
                Key = "A=A";
            }
            // Dodělat!
        }

        public virtual string Encrypt(string text, string key)
        {
            char[] zasifrovanyText = new char[text.Length];
            char[] partKey = key.ToCharArray();
            if (abeceda.Contains(partKey[0]) && abeceda.Contains(partKey[2]) && partKey[1].ToString() == "=") { 
                if(partKey[0].ToString() != partKey[2].ToString())
                {
                    char[] posunutaAbeceda = new char[abeceda.Length];
                    int rozdilPismen = FindIndexInAlphabet(partKey[2].ToString()) - FindIndexInAlphabet(partKey[0].ToString());

                    if(rozdilPismen < 0)
                    {
                        rozdilPismen = rozdilPismen + abeceda.Length;
                    }
                    Console.WriteLine(rozdilPismen);
                    for (int i = 0; i < abeceda.Length; i++)
                    {
                        posunutaAbeceda[i] = abeceda[rozdilPismen];
                        rozdilPismen++;
                        if(rozdilPismen == abeceda.Length)
                        {
                            rozdilPismen = 0;
                        }
                    }

                    // Projdeme každé písmeno v textu a nahradíme ho odpovídajícím posunutým písmenem
                    for (int i = 0; i < text.Length; i++)
                    {
                        char pismeno = text[i];
                        int index = Array.IndexOf(abeceda, pismeno);

                        if (index == -1)
                        {
                            zasifrovanyText[i] = pismeno;
                        }
                        else
                        {
                            zasifrovanyText[i] = posunutaAbeceda[index];
                        }
                    }


                }
                else
                {
                    return Encrypt(text);
                }
            } 
            else
            {
                return Encrypt(text);
            }

            return new string(zasifrovanyText);
        }

        public virtual string Decrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key == "A")
            {
                return Decrypt(text);
            }

            return text;
        }

        public int FindIndexInAlphabet(string input)
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

    }
}
