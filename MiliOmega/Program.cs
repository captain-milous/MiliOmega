namespace MiliOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Ahoj tohle je zkušební text!";
            Morseovka morseovka = new Morseovka(test);
            //morseovka.UnencryptedText = morseovka.Decrypt(morseovka.EncryptedText);
            Console.WriteLine(morseovka.ToString());

            CeaserovaSifra ceasar = new CeaserovaSifra(test,"a=c");

            Console.WriteLine(ceasar.ToString());
            string test2 = ceasar.EncryptedText;
            Console.WriteLine(test2);

            ceasar = new CeaserovaSifra(test2, "a=c", true);
            
            Console.WriteLine(ceasar.ToString());
        }
    }
}