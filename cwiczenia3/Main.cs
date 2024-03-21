using kontenerowce;
using Kontenery;

namespace cwiczenia3;

public class Consola
{
        static List<Kontener> kontenery = new List<Kontener>();
        static List<Kontenerowiec> kontenerowce = new List<Kontenerowiec>();
    public static void Main(string[] args)
    {
        while (true)
        {
            if (!Menu())
            {
                break;
            }
        }
    }

    public static void DodajKontener()
    {
        Console.WriteLine("1. Kontener na plyny");
        Console.WriteLine("2. Kontener na gaz");
        Console.WriteLine("3. Kontener chlodniczy");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1 :
                Console.WriteLine("czy produkt jest niebezpieczny? (TAK/NIE)");
                string czyNiebezpieczny = Console.ReadLine();
                KontenerNaPlyny kontener = new KontenerNaPlyny(czyNiebezpieczny);
                kontenery.Add(kontener);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("utworzono Kontener na plyny");
                Console.ResetColor();
                break;
            case 2 : 
                KontenernaGaz kontenerGaz = new KontenernaGaz();
                kontenery.Add(kontenerGaz);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("utworzono Kontener na gaz");
                Console.ResetColor();

                break;
            case 3 : 
                Console.WriteLine("Wybierz typ produktu:");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{i+1}. {Enum.GetName(typeof(TypProduktu), i)}");
                }
                int choice2;
                while (!int.TryParse(Console.ReadLine(), out choice2) || choice2 < 1 || choice2 > 10)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 10:");
                }
                TypProduktu typProduktu = (TypProduktu)choice2;  
                KontenerChlodniczy kontenerChlodniczy = new KontenerChlodniczy(typProduktu);
                kontenery.Add(kontenerChlodniczy);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("utworzono Kontener chlodniczy");
                Console.ResetColor();
                break;
                
        }
    }

    public static void FunkcjeKontenera(Kontener kontener)
    {
        Console.WriteLine("wybierz akcje:");
        Console.WriteLine("1. wyświetl informacje o kontenerze");
        Console.WriteLine("2. załaduj kontener");
        Console.WriteLine("3. rozładuj kontener");
        Console.WriteLine("4. załaduj na statek");
        Console.WriteLine("5. przeładuj kontener z statku na staek");
        Console.WriteLine("6. Zastąp kontener na statku");
        Console.WriteLine("7. Wyjdz");
        int.TryParse(Console.ReadLine(), out int choice);
        switch (choice)
        {
            case 1:
                Console.WriteLine(kontener.GetInfo());
                break;
            case 2:
                Console.WriteLine("Podaj mase do zaladowania");
                int.TryParse(Console.ReadLine(), out int masaDoZaladowania);
                kontener.Load(masaDoZaladowania);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontener załadowany");
                Console.ResetColor();
                break;
            case 3:
                kontener.EmptyLoad();
                break;
            case 4:
               Kontenerowiec kontenerowiec = WybierzKontenerowiec();
                if (kontenerowiec != null)
                {
                    kontenerowiec.getKonteners().Add(kontener);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontener załadowany na statek");
                Console.ResetColor();
                break;
            case 5:
                kontenerowiec = WybierzKontenerowiec();
                if (kontenerowiec != null)
                {
                    kontenerowiec.getKonteners().Remove(kontener);
                }
                Kontenerowiec kontenerowiec2 = WybierzKontenerowiec();
                if (kontenerowiec2 != null)
                {
                    kontenerowiec2.getKonteners().Add(kontener);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontener przeładowany ze statku na statek");
                Console.ResetColor();
                break;
            case 6:
                kontenerowiec = WybierzKontenerowiec();
                if (kontenerowiec != null)
                {
                    kontenerowiec.getKonteners().Add(kontener);
                    kontenery.Remove(kontener);
                }
                break;
            case 7:
                return;
        }
    }
    public static Kontenerowiec WybierzKontenerowiec()
    {
        Console.WriteLine("Podaj identyfikator kontenerowca");
        foreach (Kontenerowiec kontenerowiec in kontenerowce)
        {
            Console.WriteLine(kontenerowiec.getIdentifikator());
        }
        string numerSeryjny = Console.ReadLine();
        foreach (var kontenerowiec in kontenerowce)
        {
            if (kontenerowiec.getIdentifikator() == numerSeryjny)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Znaleziono kontenerowiec o podanym numerze seryjnym");
                Console.ResetColor();
                return kontenerowiec;
            }
        }
        Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym");
        return null;
    }
    public static void WyswietlKontenery()
    {
        foreach (var kontener in kontenery)
        {
            Console.WriteLine(kontener);
        }
    }
    public static Kontener WybierzKontener()
    {
        Console.WriteLine("Podaj numer seryjny kontenera");
        string numerSeryjny = Console.ReadLine();
        foreach (var kontener in kontenery)
        {
            if (kontener.GetNumerSeryjny() == numerSeryjny)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Znaleziono kontener o podanym numerze seryjnym");
                Console.ResetColor();
                return kontener;
            }
        }
        Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym");
        return null;
    }
    public static void DodajKontenerowiec()
    {
        Kontenerowiec kontenerowiec = new Kontenerowiec();
        kontenerowce.Add(kontenerowiec);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Dodano kontenerowiec: "+kontenerowiec.getIdentifikator());
        Console.ResetColor();
    }
    public static bool Menu()
    {
        Console.WriteLine("wybierz akcje:");
        Console.WriteLine("1. Dodaj kontener");
        Console.WriteLine("2. Wyswietl kontenery");
        Console.WriteLine("3. załaduj Listę kontenerów na statek");
        Console.WriteLine("4. Dodaj kontenerowiec");
        Console.WriteLine("5. Wyswietl kontenerowce");
        Console.WriteLine("6. Usun kontener ze statku");
        Console.WriteLine("7. Wybierz kontener");
        Console.WriteLine("8. Wyjdz");
        int.TryParse(Console.ReadLine(), out int choice);
        switch (choice)
        {
            case 1:
                DodajKontener();
                break;
            case 2:
                WyswietlKontenery();
                break;
            case 3:
                Console.WriteLine("wybierz kilka kontenerow do zaladowania na statek");
                WyswietlKontenery();
                List<Kontener> konteners = new List<Kontener>();
                int iloscKontenerow = 0;
                Kontener kontenerDoDodania = WybierzKontener();
                konteners.Add(kontenerDoDodania);
                iloscKontenerow++;
                Console.WriteLine("czy chcesz dodac kolejny kontener? (TAK/NIE)");
                string czyDodacKolejny = Console.ReadLine();
                while (czyDodacKolejny == "TAK")
                {
                    kontenerDoDodania = WybierzKontener();
                    konteners.Add(kontenerDoDodania);
                    iloscKontenerow++;
                    Console.WriteLine("czy chcesz dodac kolejny kontener? (TAK/NIE)");
                    czyDodacKolejny = Console.ReadLine();
                }
                Console.WriteLine("Podaj identyfikator kontenerowca");
                foreach (Kontenerowiec kontenerowiec in kontenerowce)
                {
                    Console.WriteLine(kontenerowiec.getIdentifikator());
                }
                string identyfikator = Console.ReadLine();
                foreach (var kontenerowiec in kontenerowce)
                {
                    if (kontenerowiec.getIdentifikator() == identyfikator)
                    {
                        foreach (var kontener in konteners)
                        {
                            kontenerowiec.getKonteners().Add(kontener);
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontenery dodane na statek: "+iloscKontenerow);
                Console.ResetColor();
                break;
            case 4:
                 DodajKontenerowiec();
                break;
            case 5:
                foreach (var kontenerowiec in kontenerowce)
                {
                    Console.WriteLine(kontenerowiec);
                }
                break;
            case 6:
                Console.WriteLine("Podaj numer seryjny kontenera do usuniecia");
                string numerSeryjny = Console.ReadLine();
                foreach (var kontenerowiec in kontenerowce)
                {
                    foreach (var kontener in kontenerowiec.getKonteners())
                    {
                        if (kontener.GetNumerSeryjny() == numerSeryjny)
                        {
                            kontenerowiec.getKonteners().Remove(kontener);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("usunieto kontener");
                            Console.ResetColor();
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie znaleziono kontenera na żadnym statku");
                Console.ResetColor();
                break;
            case 7:
                WyswietlKontenery();
                FunkcjeKontenera(WybierzKontener());
                break;
            case 8:
                return false;
        }
        return true;
    }
}