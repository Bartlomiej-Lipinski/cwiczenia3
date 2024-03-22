using ContainerShips;
using Containers;

namespace cwiczenia3;

public class Consola
{
        static List<Container> _containers = new List<Container>();
        static List<ContainerShip> _containerShips = new List<ContainerShip>();
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

    public static void AddContainer()
    {
        Console.WriteLine("1. Kontener na plyny");
        Console.WriteLine("2. Kontener na gaz");
        Console.WriteLine("3. Kontener chlodniczy");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1 :
                Console.WriteLine("czy produkt jest niebezpieczny? (TAK/NIE)");
                string isHazardous = Console.ReadLine();
                LiquidContainer liquidContainer = new LiquidContainer(isHazardous);
                _containers.Add(liquidContainer);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("utworzono Kontener na plyny");
                Console.ResetColor();
                break;
            case 2 : 
                ContainerForGas gasContainer = new ContainerForGas();
                _containers.Add(gasContainer);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("utworzono Kontener na gaz");
                Console.ResetColor();

                break;
            case 3 : 
                Console.WriteLine("Wybierz typ produktu:");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{i+1}. {Enum.GetName(typeof(ProductType), i)}");
                }
                int choice2;
                while (!int.TryParse(Console.ReadLine(), out choice2) || choice2 < 1 || choice2 > 10)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 10:");
                }
                ProductType productType = (ProductType)choice2;  
                ContainerWithFreezer containerWithFreezer = new ContainerWithFreezer(productType);
                _containers.Add(containerWithFreezer);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("utworzono Kontener chlodniczy");
                Console.ResetColor();
                break;
                
        }
    }

    public static void ContainerFunctions(Container container)
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
                Console.WriteLine(container.GetInfo());
                break;
            case 2:
                Console.WriteLine("Podaj mase do zaladowania");
                int.TryParse(Console.ReadLine(), out int masaDoZaladowania);
                container.Load(masaDoZaladowania);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontener załadowany");
                Console.ResetColor();
                break;
            case 3:
                container.EmptyLoad();
                break;
            case 4:
               ContainerShip containerShip = ChoseContainerShip();
                if (containerShip != null)
                {
                    containerShip.GetContainers().Add(container);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontener załadowany na statek");
                Console.ResetColor();
                break;
            case 5:
                containerShip = ChoseContainerShip();
                if (containerShip != null)
                {
                    containerShip.GetContainers().Remove(container);
                }
                ContainerShip containerShip2 = ChoseContainerShip();
                if (containerShip2 != null)
                {
                    containerShip2.GetContainers().Add(container);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontener przeładowany ze statku na statek");
                Console.ResetColor();
                break;
            case 6:
                containerShip = ChoseContainerShip();
                if (containerShip != null)
                {
                    containerShip.GetContainers().Add(container);
                    _containers.Remove(container);
                }
                break;
            case 7:
                return;
        }
    }
    public static ContainerShip ChoseContainerShip()
    {
        Console.WriteLine("Podaj identyfikator kontenerowca");
        foreach (ContainerShip containerShip in _containerShips)
        {
            Console.WriteLine(containerShip.GetIdentifikator());
        }
        string serialNumber = Console.ReadLine();
        foreach (var choseContainerShip in _containerShips)
        {
            if (choseContainerShip.GetIdentifikator() == serialNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Znaleziono kontenerowiec o podanym numerze seryjnym");
                Console.ResetColor();
                return choseContainerShip;
            }
        }
        Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym");
        return null;
    }
    public static void DisplayExistingContainers()
    {
        foreach (var kontener in _containers)
        {
            Console.WriteLine(kontener);
        }
    }
    public static Container ChoseContainer()
    {
        Console.WriteLine("Podaj numer seryjny kontenera");
        string serialNumber = Console.ReadLine();
        foreach (var choseContainer in _containers)
        {
            if (choseContainer.GetSerialNumber() == serialNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Znaleziono kontener o podanym numerze seryjnym");
                Console.ResetColor();
                return choseContainer;
            }
        }
        Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym");
        return null;
    }
    public static void AddContainerShip()
    {
        ContainerShip containerShip = new ContainerShip();
        _containerShips.Add(containerShip);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Dodano kontenerowiec: "+containerShip.GetIdentifikator());
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
                AddContainer();
                break;
            case 2:
                DisplayExistingContainers();
                break;
            case 3:
                Console.WriteLine("wybierz kilka kontenerow do zaladowania na statek");
                DisplayExistingContainers();
                List<Container> containers = new List<Container>();
                int contenerCount = 0;
                Container containerToAdd = ChoseContainer();
                containers.Add(containerToAdd);
                contenerCount++;
                Console.WriteLine("czy chcesz dodac kolejny kontener? (TAK/NIE)");
                string ifContinue = Console.ReadLine();
                while (ifContinue == "TAK")
                {
                    containerToAdd = ChoseContainer();
                    containers.Add(containerToAdd);
                    contenerCount++;
                    Console.WriteLine("czy chcesz dodac kolejny kontener? (TAK/NIE)");
                    ifContinue = Console.ReadLine();
                }
                Console.WriteLine("Podaj identyfikator kontenerowca");
                foreach (ContainerShip containerShip in _containerShips)
                {
                    Console.WriteLine(containerShip.GetIdentifikator());
                }
                string identificator = Console.ReadLine();
                foreach (var containerShip in _containerShips)
                {
                    if (containerShip.GetIdentifikator() == identificator)
                    {
                        foreach (var container in containers)
                        {
                            containerShip.GetContainers().Add(container);
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontenery dodane na statek: "+contenerCount);
                Console.ResetColor();
                break;
            case 4:
                 AddContainerShip();
                break;
            case 5:
                foreach (var containerShip in _containerShips)
                {
                    Console.WriteLine(containerShip);
                }
                break;
            case 6:
                Console.WriteLine("Podaj numer seryjny kontenera do usuniecia");
                string serialNumber = Console.ReadLine();
                foreach (var containerShip in _containerShips)
                {
                    foreach (var container in containerShip.GetContainers())
                    {
                        if (container.GetSerialNumber() == serialNumber)
                        {
                            containerShip.GetContainers().Remove(container);
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
                DisplayExistingContainers();
                ContainerFunctions(ChoseContainer());
                break;
            case 8:
                return false;
        }
        return true;
    }
}