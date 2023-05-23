namespace MiliOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Ahoj tohle je zkušební text!";
            Morseovka morseovka = new Morseovka();
            morseovka.UnencryptedText = "Simba je užasný pejsek. Máme ho všichni moc rádi. Jen Kiki ho pořád zlobí.";
            //morseovka.UnencryptedText = morseovka.Decrypt(morseovka.EncryptedText);
            Console.WriteLine(morseovka.ToString());

            CeaserovaSifra ceasar = new CeaserovaSifra(test,"a=c");

            Console.WriteLine(ceasar.ToString());
        }
    }
}