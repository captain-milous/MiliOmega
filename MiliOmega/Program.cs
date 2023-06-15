namespace MiliOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Ahoj tohle je zkušební text!";
            string test2 = "Tohle je drůhý test!";

            /*
            Console.WriteLine("Morseovka");
            
            Morseovka morseovka = new Morseovka(test);
            Console.WriteLine(morseovka.ToString());
            test2 = "-|---|....|.-..|.||.---|.||-..|.-.|..-|....|-.--||-|.|...|-|||";
            morseovka = new Morseovka(test2, true);
            Console.WriteLine(morseovka.ToString());
            
            Console.WriteLine();
            Console.WriteLine("Ceaserova Sifra");

            CeaserovaSifra ceasar = new CeaserovaSifra(test,"a=c");
            Console.WriteLine(ceasar.ToString());
            test2 = "VQJNG LG FTWJA VGUV!";
            ceasar = new CeaserovaSifra(test2, "a=c", true);
            Console.WriteLine(ceasar.ToString());

            Console.WriteLine();
            Console.WriteLine("Cisla Misto Pismen");

            CislaMistoPismen cp = new CislaMistoPismen(test);
            Console.WriteLine(cp.ToString());
            test2 = "20 15 8 12 5 10 5 4 18 21 8 25 20 5 19 20!";
            cp = new CislaMistoPismen(test2, true);
            Console.WriteLine(cp.ToString());

            Console.WriteLine();
            Console.WriteLine("Mezerova sifra");

            Mezerova mez = new Mezerova(test);
            Console.WriteLine(mez.ToString());
            test2 = "SU NP GI KM DF IK DF CE QS TV GI XZ SU DF RT SU !";
            mez = new Mezerova(test2, true);
            Console.WriteLine(mez.ToString());
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            */
            string oddelovac = "\n----------------------------------------------------------------------------------\n";
            Console.WriteLine(oddelovac);
            Console.WriteLine("Vítejte v programu pro šifrování textu 1.0!");
            Console.WriteLine("Autor: Miloš Tesař C3b");
            Console.WriteLine(oddelovac);

            new UserHandler("localhost", "SPSEJecna", "miliomega", "Heslo1234");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Konec Programu");
            Console.WriteLine(oddelovac);

        }
    }
}