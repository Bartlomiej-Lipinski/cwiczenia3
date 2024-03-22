using Containers;

namespace Containers
{
    public abstract class Container
    {
        private double _cargoMass;

        public double GetCargoMass()
        {
            return _cargoMass;
        }

        public void SetCargoMass(double value)
        {
            _cargoMass = value;
        }

        private int _highth;
        private double _containerOwnMass;

        public double GetContainerOwnMass()
        {
            return _containerOwnMass;
        }

        private int _depth;
        private string _serialNumber;

        public String GetSerialNumber()
        {
            return _serialNumber;
        }

        private double _maxLoad;

        public double GetMaxLoad()
        {
            return _maxLoad;
        }

        public void SetMaxLoad(double value)
        {
            _maxLoad = value;
        }

        private bool _isHazardous;

        public bool GetIsHazardous()
        {
            return _isHazardous;
        }

        public void SetIsHazardous(bool value)
        {
            _isHazardous = value;
        }

        public Container()
        {
            _cargoMass = _containerOwnMass + _cargoMass;
            _highth = 3;
            _containerOwnMass = 2350;
            _depth = 6;
            _serialNumber = "KON-" + EstablishConType() + "-" + GenerateSn();
            _maxLoad = 25_000;
        }

        public virtual void EmptyLoad()
        {
            _cargoMass = _containerOwnMass;
        }

        public virtual void Load(int massToLoad)
        {
            if (_cargoMass + _containerOwnMass + massToLoad <= _maxLoad)
            {
                _cargoMass += massToLoad;
            }
            else
            {
                try
                {
                    throw new OverFilledException();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private int GenerateSn()
        {
            return new Random().Next(1, 9999);
        }

        private string EstablishConType()
        {
            if (this is LiquidContainer)
            {
                return "L";
            }

            if (this is ContainerForGas)
            {
                return "G";
            }

            if (this is ContainerWithFreezer)
            {
                return "C";
            }

            return "N";
        }

        public override string ToString()
        {
            return "Numer seryjny: " + _serialNumber + " Masa ladunku: " + _cargoMass;
        }

        public virtual String GetInfo()
        {
            return "Numer seryjny: " + _serialNumber + " Masa ladunku: " + _cargoMass + " Wysokosc: " + _highth +
                   " Glebokosc: " + _depth + " Waga wlasna: " + _containerOwnMass + " Maksymalna ladownosc: " +
                   _maxLoad + " Czy jest niebezpieczny: " + _isHazardous;
        }
    }

    internal class OverFilledException : Exception
    {
        public OverFilledException()
        {
            Console.WriteLine("Kontener jest przeciażony!");
        }
    }

    public class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(String setHazardus) : base()
        {
            if (setHazardus == "TAK")
            {
                SetIsHazardous(true);
            }
            else
            {
                SetIsHazardous(false);
            }
        }

        public override void Load(int massToLoad)
        {
            if (GetIsHazardous())
            {
                SetMaxLoad(GetMaxLoad() * 0.5);
            }
            else
            {
                SetMaxLoad(GetMaxLoad() * 0.9);
            }

            if (GetCargoMass() + GetContainerOwnMass() > GetMaxLoad())
            {
                IHazardNotifier.NotifyHazard(GetSerialNumber());
            }

            base.Load(massToLoad);
        }
    }

    public class ContainerForGas : Container, IHazardNotifier
    {
        private int Pressure;

        public ContainerForGas() : base()
        {
            Pressure = new Random().Next(1, 100);
        }

        public override void EmptyLoad()
        {
            SetCargoMass(GetCargoMass() * 0.05);
        }
    }

    public class ContainerWithFreezer : Container
    {
        private double _temp;
        private ProductType _productType { get; set; }


        public ContainerWithFreezer(ProductType _productType) : base()
        {
            this._productType = _productType;
            EstablishTemp(_productType);
        }

        private void EstablishTemp(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Bananas:
                    _temp = 13.5;
                    break;
                case ProductType.Chocolate:
                    _temp = 18;
                    break;
                case ProductType.Fish:
                    _temp = 2;
                    break;
                case ProductType.Meat:
                    _temp = -15;
                    break;
                case ProductType.IceCream:
                    _temp = -18;
                    break;
                case ProductType.FrozenPizza:
                    _temp = -30;
                    break;
                case ProductType.Cheese:
                    _temp = 7.2;
                    break;
                case ProductType.Sausages:
                    _temp = 5;
                    break;
                case ProductType.Butter:
                    _temp = 20.5;
                    break;
                case ProductType.Eggs:
                    _temp = 19;
                    break;
            }
        }

        public String GetInfo()
        {
            return base.GetInfo() + " Temperatura: " + _temp + " Typ produktu: " + _productType;
        }
    }

    public interface IHazardNotifier
    {
        public static void NotifyHazard(String sn)
        {
            Console.WriteLine("Uwaga! Kontener zawiera materiały niebezpieczne!" + " Numer seryjny: " + sn);
        }
    }

    public enum ProductType
    {
        Bananas,
        Chocolate,
        Fish,
        Meat,
        IceCream,
        FrozenPizza,
        Cheese,
        Sausages,
        Butter,
        Eggs
    }
}

namespace ContainerShips
{
    public class ContainerShip
    {
        public static int number = 0;
        private string _Identifikator;
        private List<Container> _containers;
        private int _maxSpeed;
        private int _maxContainerOnBoard;
        private int _maxLoad;

        public ContainerShip()
        {
            _maxSpeed = new Random().Next(1, 25);
            _maxContainerOnBoard = new Random().Next(10_000, 24_000);
            _maxLoad = 24_000 * 25_000;
            _containers = new List<Container>();
            _Identifikator = "SHIP-" + number;
            number++;
        }

        public void AddContainer(Container container)
        {
            _containers.Add(container);
        }

        public override string ToString()
        {
            return "Identyfikator: " + _Identifikator + " Maksymalna predkosc: " + _maxSpeed +
                   " Maksymalna ilosc kontenerow: " + _maxContainerOnBoard + " Maksymalna ladownosc: " + _maxLoad +
                   " Aktualna ilosc kontenerow: " + _containers.Count;
        }

        public String GetIdentifikator()
        {
            return _Identifikator;
        }

        public List<Container> GetContainers()
        {
            return _containers;
        }
    }
}