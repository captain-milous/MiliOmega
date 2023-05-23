using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliOmega
{
    public class CislaMistoPismen
    {
        private char[] abeceda = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();



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
            return string.Empty;
        }
    }
}
