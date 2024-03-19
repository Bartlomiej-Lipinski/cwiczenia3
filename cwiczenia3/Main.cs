using System.Collections;
using kontenerowce;
using Kontenery;

namespace cwiczenia3;

public class Consola
{
        static List<Kontener> kontenery = new List<Kontener>();
        static ArrayList kontenerowce = new ArrayList();
    public static void Main(string[] args)
    {
        while (true)
        {
            if (!menu())
            {
                break;
            }
        }
    }

    public static void dodajKontener()
    {
        Console.WriteLine("1. Kontener na plyny");
        Console.WriteLine("2. Kontener na gaz");
        Console.WriteLine("3. Kontener chlodniczy");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1 :
                KontenerNaPlyny kontener = new KontenerNaPlyny();
                kontenery.Add(kontener);
                Console.WriteLine("utworzono Kontener na plyny");
                break;
            case 2 : 
                KontenernaGaz kontenerGaz = new KontenernaGaz();
                kontenery.Add(kontenerGaz);
                Console.WriteLine("utworzono Kontener na gaz");
                break;
            case 3 : 
                Console.WriteLine("Wybierz typ produktu:");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(TypProduktu), i)}");
                }
                int choice2;
                while (!int.TryParse(Console.ReadLine(), out choice2) || choice2 < 1 || choice2 > 10)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 10:");
                }
                TypProduktu typProduktu = (TypProduktu)choice2;  
                KontenerChlodniczy kontenerChlodniczy = new KontenerChlodniczy(typProduktu);
                kontenery.Add(kontenerChlodniczy);
                Console.WriteLine("utworzono Kontener chlodniczy");
                break;
                
        }
    }

    public static void funkcjeKontenera()
    {
        Console.WriteLine("wybierz akcje:");
        Console.WriteLine("wyświetl informacje o kontenerze");
        Console.WriteLine("załaduj kontener");
        Console.WriteLine("rozładuj kontener");
        Console.WriteLine("przeładuj kontener z statku na staek");
        
    }
    public static void wyswietlKontenery()
    {
        foreach (var kontener in kontenery)
        {
            Console.WriteLine(kontener);
        }
    }
    public static void usunKontener()
    {
        Console.WriteLine("Podaj numer seryjny kontenera do usuniecia");
        string numerSeryjny = Console.ReadLine();
        foreach (var kontener in kontenery)
        {
            if (kontener.getNumerSeryjny() == numerSeryjny)
            {
                kontenery.Remove(kontener);
                Console.WriteLine("usunieto kontener");
                return;
            }
        }
        Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym");
    }
    public static bool menu()
    {
        Console.WriteLine("wybierz akcje:");
        Console.WriteLine("1. Dodaj kontener");
        Console.WriteLine("2. Wyswietl kontenery");
        Console.WriteLine("3. Usun kontener");
        Console.WriteLine("4. Dodaj kontenerowiec");
        Console.WriteLine("5. Wyswietl kontenerowce");
        Console.WriteLine("6. Usun kontenerowiec");
        Console.WriteLine("7. Wyjdz");
        int.TryParse(Console.ReadLine(), out int choice);
        switch (choice)
        {
            case 1:
                dodajKontener();
                break;
            case 2:
                wyswietlKontenery();
                break;
            case 3:
                usunKontener();
                break;
            case 4:
                // dodajKontenerowiec();
                break;
            case 7:
                return false;
            break;
            default:
                break;
        }

        return true;
    }
}