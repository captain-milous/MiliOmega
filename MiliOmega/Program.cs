namespace MiliOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            Morseovka morseovka = new Morseovka();
            morseovka.UnencryptedText = "Ahoj jak se máš? Já se mám skvěle.";
            //morseovka.UnencryptedText = morseovka.Decrypt(morseovka.EncryptedText);
            Console.WriteLine(morseovka.ToString());
        }
    }
}