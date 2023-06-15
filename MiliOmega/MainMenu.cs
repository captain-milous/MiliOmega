using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiliOmega.Methods;

namespace MiliOmega
{
    public class MainMenu
    {
        public static void Start()
        {
            string oddelovac = "\n----------------------------------------------------------------------------------\n";
            Console.WriteLine(oddelovac);
            int input = 0;
            int pocetAktSifer = 4;
            int strike = 1;
            int maxStrike = 3;
            bool run = true;
            while (run)
            {
                Console.WriteLine("Hlavní Menu\n1 - Naše Šifry\n2 - Zašifrovat text z konzole\n3 - Zašifrovat text ze souboru\n4 - Rozšifrovat text ze souboru\n5 - Ukončit program");
                Console.Write("Vyberte možnost: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                    input = 0;
                }
                Console.WriteLine();
                switch (input)
                {
                    case 1:
                        Console.WriteLine(oddelovac);
                        Console.WriteLine("\nŠifry, které nabízíme:\n\n");
                        Console.WriteLine("1 - Morseova šifra\n  Morseova abeceda je skupina symbolů, která je používána v telegrafii. \n  Kóduje znaky latinské abecedy, číslice a speciální znaky do kombinací krátkých a dlouhých signálů. \n  Ty je možné přenášet na dálku jednodušším způsobem, než všechny znaky abecedy.\n  Např.: AHOJ = .-|....|---|.---|\n\n");
                        Console.WriteLine("2 - Caesarova šifra\n  Princip Caesarovy šifry je založen na tom, že všechna písmena zprávy jsou během šifrování zaměněna za písmeno, které se abecedně nachází o pevně určený počet míst dále (tj. posun je pevně zvolen).\n  Počet možných variant klíče této šifry je o jedna menší než počet písmen (znaků) v použité abecedě.\n  Např.: AHOJ (s klíčem A=H) = HOVQ, nebo (s klíčem Z=A) = BIPK\n\n");
                        Console.WriteLine("3 - Mezerová šifra\n  Princip je ten, že zašifrovaná písmena jsou dvojice písmen (první je v abecedě před nezašifrovaným a druhé je po něm) \n  Např.: AHOJ = ZB GI NP IK\n\n");
                        Console.WriteLine("4 - Čisla místo písmen\n  Šifra, kde jsou místo písmen čísla (index v abecedě) \n  Např.: AHOJ = 1 8 15 10\n");
                        Console.WriteLine(oddelovac);
                        break;
                    case 2:
                        Console.WriteLine(oddelovac);
                        Console.Write("Zadejte text, který chcete zašifrovat: ");
                        string uzivatelText = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("1 - Morseova šifra\n2 - Caesarova šifra\n3 - Mezerová šifra\n4 - Čisla místo písmen");
                        Console.Write("Vyberte, kterou šifrou chcete šifrovat: ");
                        int typSifry = 0;
                        try
                        {
                            typSifry = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Zadaná Hodnota musí být integer!");
                            typSifry = 0;
                        }
                        if(input > 0 && input < pocetAktSifer)
                        {
                            string output = Methods.ChooseAndEncrypt(uzivatelText, typSifry);
                            string[] visible = output.Split('%');
                            Console.WriteLine("\nZašifrovaný text: " + visible[1]);
                            Console.WriteLine("\nChcete text uložit do souboru?\n1 - Ano\n2 - Ne");
                            Console.Write("Vyberte možnost: ");
                            try
                            {
                                input = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Zadaná Hodnota musí být integer!");
                                input = 0;
                            }
                            if (input == 1)
                            {
                                Methods.Export(output);
                            }
                        }
                        Console.WriteLine(oddelovac);
                        break;
                    case 3:
                        Console.WriteLine(oddelovac);
                        uzivatelText = Methods.Import();
                        Console.WriteLine();
                        Console.WriteLine("1 - Morseova šifra\n2 - Caesarova šifra\n3 - Mezerová šifra\n4 - Čisla místo písmen");
                        Console.Write("Vyberte, kterou šifrou chcete šifrovat: ");
                        try
                        {
                            typSifry = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Zadaná Hodnota musí být integer!");
                            typSifry = 0;
                        }
                        if (input > 0 && input < pocetAktSifer)
                        {
                            string output = Methods.ChooseAndEncrypt(uzivatelText, typSifry);
                            string[] visible = output.Split('%');
                            Console.WriteLine("\nZašifrovaný text: " + visible[1]);
                            Console.WriteLine("\nChcete text uložit do souboru?\n1 - Ano\n2 - Ne");
                            Console.Write("Vyberte možnost: ");
                            try
                            {
                                input = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Zadaná Hodnota musí být integer!");
                                input = 0;
                            }
                            if (input == 1)
                            {
                                Methods.Export(output);
                            }
                        }
                        Console.WriteLine(oddelovac);
                        break;
                    case 4:
                        Console.WriteLine(oddelovac);
                        Console.WriteLine("Omlouváme se, ale tato funkce ještě není k dispozici. \nKontaktujte vývojáře nebo zkuste vyšší verzi programu, pokud je k dispozici");/*
                        string importedText = Methods.Import();
                        string[] raw = importedText.Split('%');
                        string decrypted = string.Empty;
                        Console.WriteLine(raw.Length);
                        if (raw.Length == 2) 
                        {
                            try
                            {
                                decrypted = Methods.DecryptFromFile(raw[0], raw[1]);
                                Console.WriteLine(decrypted);
                            } 
                            catch (Exception e) 
                            {
                                Console.WriteLine("Chyba");
                            }
                        } 
                        else
                        {
                            Console.WriteLine("Textový soubor není vhodný pro překlad zpět.");
                        }
                        Console.WriteLine();*/
                        Console.WriteLine(oddelovac);
                        break;
                    case 5:
                        run = false;
                        break;
                    default:
                        if (strike < maxStrike)
                        {
                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků program se automaticky ukončí.");
                            Console.WriteLine("Vyberte možnost z nabídky!");
                            Console.WriteLine(oddelovac);
                            strike++;
                        }
                        else
                        {
                            Console.WriteLine("Dosáhli jste " + strike + " striků. Program se teď automaticky ukončí.");
                            run = false;
                        }
                        break;
                }
            }
        }
        



    }
}
