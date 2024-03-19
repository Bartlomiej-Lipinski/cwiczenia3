using System.Collections;
using kontenerowce;
using Kontenery;

namespace cwiczenia3;

public class Consola
{
    public static void Main(string[] args)
    {
        ArrayList kontenerowce = new ArrayList();
        ArrayList kontenery = new ArrayList();
        Console.WriteLine("wybierz akcje:");
        Console.WriteLine("1. Dodaj kontener");
        Console.WriteLine("2. Wyswietl kontenery");
        Console.WriteLine("3. Usun kontener");
        
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
                KontenerNaPlyny kontener = new KontenerNaPlyny(new Random().Next(1000, 5000));
                Console.WriteLine("utworzono kontener na Kontener na plyny");
                break;
            case 2 : Console.WriteLine("Kontener na gaz");break;
            case 3 : Console.WriteLine("Kontener chlodniczy");break;
                
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
}