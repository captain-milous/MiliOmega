using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MiliOmega
{
    public class Methods
    {
        public enum TypSifry
        {
            None,
            Morseovka,
            Mezerovka,
            Ceaserova,
            CislaMistoPismen
        }
        /// <summary>
        /// Vrací všechen text ze zadaného textového souboru který je ve složce Import. Pokud textový soubor neexistuje nebo je prázdný, tav vrátí string.empty
        /// </summary>
        /// <returns>text ze zadaného textového souboru</returns>
        public static string Import()
        {
            string importniSlozka = "Import";

            Console.Write("Zadejte název souboru:");
            string nazevSouboru = Console.ReadLine().ToLower();

            if (!nazevSouboru.EndsWith(".txt"))
            {
                nazevSouboru += ".txt";
            }

            string cesta = Path.Combine(importniSlozka, nazevSouboru);

            if (File.Exists(cesta) && new FileInfo(cesta).Length > 0)
            {
                try
                {
                    return File.ReadAllText(cesta);
                }
                catch (IOException)
                {
                    Console.WriteLine("Při čtení souboru došlo k chybě. Zkuste to prosím znovu.");
                }
            }
            else
            {
                Console.WriteLine("Soubor neexistuje nebo je prázdný.");
            }

            return string.Empty;
        }
        /// <summary>
        /// Ukládá do složky Export textové soubory
        /// </summary>
        /// <param name="text">text který chceme uložit</param>
        public static void Export(string text) 
        {
            Console.Write("Zadejte název souboru pro export:");
            string nazevSouboru = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(nazevSouboru))
            {
                Console.WriteLine("Neplatný název souboru. Export byl zrušen.");
                return;
            }

            if (!nazevSouboru.EndsWith(".txt"))
            {
                nazevSouboru += ".txt";
            }

            string exportniSlozka = "Export";
            string cesta = Path.Combine(exportniSlozka, nazevSouboru);

            if (File.Exists(cesta))
            {
                Console.WriteLine("Soubor s tímto názvem již existuje. Export byl zrušen.");
                return;
            }

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Text pro export je prázdný. Export byl zrušen.");
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(cesta))
                {
                    writer.WriteLine(text);
                }

                Console.WriteLine("Text byl úspěšně exportován do souboru.");
            }
            catch (IOException)
            {
                Console.WriteLine("Při exportu do souboru došlo k chybě. Zkuste to prosím znovu.");
            }
        }

        public static string ChooseAndEncrypt(string text, int typ)
        {
            TypSifry sifrovani = TypSifry.None;
            switch(typ)
            {
                case 1:
                    sifrovani = TypSifry.Morseovka;
                    break;
                case 2:
                    sifrovani = TypSifry.Ceaserova;
                    break;
                case 3:
                    sifrovani = TypSifry.Mezerovka;
                    break;
                case 4:
                    sifrovani = TypSifry.CislaMistoPismen;
                    break;
                default: 
                    return text;
            }
            return Encrypt(text,sifrovani);
        }

        static string Encrypt(string text, TypSifry sifrovani)
        {
            string output = string.Empty;
            switch (sifrovani)
            {
                case TypSifry.Morseovka:
                    output = "01:MOR^null%\n";
                    Morseovka mor = new Morseovka(text);
                    output += mor.GetEncryptedText();
                    break;
                case TypSifry.Ceaserova:
                    Console.Write("Zadejte klíč pro zašifrování (ve tvaru A=X): ");
                    string key = Console.ReadLine().ToUpper();
                    CeaserovaSifra csr = new CeaserovaSifra(text, key);
                    output = "01:CSR^" + csr.GetKey() +"%\n";
                    output += csr.GetEncryptedText();
                    break;
                case TypSifry.Mezerovka:
                    output = "01:MEZ^null%\n";
                    Mezerova mez = new Mezerova(text);
                    output += mez.GetEncryptedText();
                    break;
                case TypSifry.CislaMistoPismen:
                    output = "01:CMP^null%\n";
                    CislaMistoPismen cmp = new CislaMistoPismen(text);
                    output += cmp.GetEncryptedText();
                    break;
                default:
                    return text;
            }

            return output;
        }

        public static string DecryptFromFile(string rawKey, string encrypted)
        {
            string output = string.Empty;
            string sifra = string.Empty;
            string key = string.Empty;
            string[] first = rawKey.Split(':');
            if (first.Length == 2)
            {
                int ver = 0;
                try
                {
                    ver = Convert.ToInt32(first[0]);
                } 
                catch(Exception e) 
                {
                    Console.WriteLine("Špatná forma klíče");
                }
                if (ver == 1)
                {
                    string second = first[1];
                    string[] third = second.Split('^');
                    if (third.Length == 2)
                    {
                        sifra = third[0].ToUpper();
                        key = third[1].ToUpper();
                    }
                }
                else
                {
                    return "Nekompatibilní verze klíče";
                }
            }

            switch (sifra)
            {
                case "MOR":
                    Morseovka mor = new Morseovka(encrypted, true);
                    output = mor.GetEncryptedText();
                    break;
                case "CSR":
                    CeaserovaSifra csr = new CeaserovaSifra(encrypted, key, true);
                    output = csr.GetEncryptedText();
                    break;
                case "MEZ":
                    Mezerova mezerova= new Mezerova(encrypted, true);
                    output = mezerova.GetEncryptedText();
                    break;
                case "CMP":
                    CislaMistoPismen cmp = new CislaMistoPismen(encrypted, true);
                    output = cmp.GetEncryptedText();
                    break;
                default:
                    return "Špatná forma klíče";

            }
            return output;
        }


    }
}