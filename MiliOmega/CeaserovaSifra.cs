﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class CeaserovaSifra : Sifra
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        public CeaserovaSifra(string text, string key)
        {
            RawText = text;
            UnencryptedText = SimplifyRawText(RawText);

            key.Replace(" ", "");
            if (key.Length == 3)
            {
                Key = SimplifyRawText(key);
            }
            else
            {
                Key = "A=A";
            }
            EncryptedText = Encrypt(UnencryptedText, Key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="deciphering"></param>
        public CeaserovaSifra(string text, string key, bool deciphering)
        {
            RawText = text;
            key.Replace(" ", "");
            if (key.Length == 3)
            {
                Key = SimplifyRawText(key);
            } 
            else
            {
                Key = "A=A";
            }

            if (!deciphering)
            {
                UnencryptedText = SimplifyRawText(RawText);
                EncryptedText = Encrypt(UnencryptedText, Key);
            }
            else
            {
                EncryptedText = SimplifyRawText(RawText);
                UnencryptedText = Decrypt(EncryptedText, Key);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string Encrypt(string text, string key)
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

        public override string Decrypt(string text, string key)
        {
            char[] rozsifrovanyText = new char[text.Length];
            char[] partKey = key.ToCharArray();
            Console.WriteLine(key);
            if (abeceda.Contains(partKey[0]) && abeceda.Contains(partKey[2]) && partKey[1].ToString() == "=")
            {
                if (partKey[0].ToString() != partKey[2].ToString())
                {
                    char[] posunutaAbeceda = new char[abeceda.Length];
                    int rozdilPismen = FindIndexInAlphabet(partKey[2].ToString()) - FindIndexInAlphabet(partKey[0].ToString());

                    if (rozdilPismen < 0)
                    {
                        rozdilPismen = rozdilPismen + abeceda.Length;
                    }
                    Console.WriteLine(rozdilPismen);
                    for (int i = 0; i < abeceda.Length; i++)
                    {
                        posunutaAbeceda[i] = abeceda[rozdilPismen];
                        rozdilPismen++;
                        if (rozdilPismen == abeceda.Length)
                        {
                            rozdilPismen = 0;
                        }
                    }
                    // změnit
                    for (int i = 0; i < text.Length; i++)
                    {
                        char pismeno = text[i];
                        int index = Array.IndexOf(posunutaAbeceda, pismeno);

                        if (index == -1)
                        {
                            rozsifrovanyText[i] = pismeno;
                        }
                        else
                        {
                            rozsifrovanyText[i] = abeceda[index];
                        }
                    }
                }
                else
                {
                    return SimplifyRawText(text);
                }
            }
            else
            {
                return "Neplatný klíč!";
            }

            return new string(rozsifrovanyText);
        }


    }
}
