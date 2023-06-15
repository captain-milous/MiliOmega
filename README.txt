Šifrování textu 1.0 (MiliOmega)

Popis projektu
Je to konzolová aplikace napsaná v jazyce C#, která umožňuje uživatelům pracovat s různými šifrovacími metodami. Software je navržen tak, aby byl spustitelný na jakémkoli počítači a poskytoval smysluplné reálné použití.

Instalace
- Naklonujte tento repozitář na své lokální zařízení.
- Otevřete projekt vývojového prostředí (např. Visual Studio).
- Proveďte kompilaci projektu.

Použití
- Spusťte aplikaci pomocí spustitelného souboru.

- Aplikace se pokusí připojit k databázi s uživateli této aplikace. Pokud se připojení podaří, zobrazí se přihlašovací menu s možnostmi Přihlásit se, Registrovat nebo Pokračovat jako host.

- Po úspěšném přihlášení se dostanete do hlavní nabídky, kde si můžete vybrat z následujících možností:

	1) Naše Šifry - Zobrazí všechny dostupné šifrovací metody, jako je Morseova šifra, Caesarova šifra, Mezerová šifra a Čísla místo písmen.

		- Morseova šifra: Kóduje znaky latinské abecedy, číslice a speciální znaky do kombinací krátkých a dlouhých signálů. Například: AHOJ = .-|....|---|.---|

		- Caesarova šifra: Při šifrování se písmena zaměňují za písmeno, které se abecedně nachází o pevně určený počet míst dále. Například: AHOJ (s klíčem A=H) = HOVQ nebo (s klíčem Z=A) = BIPK

		- Mezerová šifra: Zašifrovaná písmena jsou dvojice písmen, přičemž první je v abecedě před nezašifrovaným a druhé je po něm. Například: AHOJ = ZB GI NP IK

		- Čísla místo písmen: Místo písmen jsou použita čísla, která odpovídají jejich indexu v abecedě. Například: AHOJ = 1 8 15 10

	2) Zašifrovat text z konzole (Můžete si zvolit z výše uvedených šifer)

	3) Zašifrovat text ze souboru (Můžete si zvolit z výše uvedených šifer)

	4) Rozšifrovat text ze souboru (Pouze soubory, které program vytvořil. Mají v sobě klíč kterým se může šifra rozšifrovat nazpět do prostého textu.)

	5) Ukončit program



Kontakt

Pro jakékoliv dotazy nebo připomínky k projektu můžete kontaktovat autora:

Autor: Miloš Tesař C3b
Email: tesar@spsejecna.cz