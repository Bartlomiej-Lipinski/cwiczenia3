using System.Linq.Expressions;
using Kontenery;

namespace Kontenery
{
    public abstract class Kontener
    {
        private double masaLadunku;
        public double GetMasaLadunku()
        {
            return masaLadunku;
        }
        public void SetMasaLadunku(double value)
        {
            masaLadunku = value;
        }
        private int wysokosc;
        private double WagaWlasna;
        public double GetWagaWlasna()
        {
            return WagaWlasna;
        }
        private int glebokosc;
        private string numerSeryjny;
        public String GetNumerSeryjny()
        {
            return numerSeryjny;
        }
        private double maxLadownosc;
        public double GetmaxLadownosc()
        {
            return maxLadownosc;
        }
        public void SetmaxLadownosc(double value)
        {
            maxLadownosc = value;
        }

        private bool isHazardous;
        public bool GetIsHazardous()
        {
            return isHazardous;
        }
        public void SetIsHazardous(bool value)
        {
            isHazardous = value;
        }

        public Kontener()
        {
            masaLadunku = WagaWlasna + masaLadunku ;
            wysokosc = 3;
            WagaWlasna = 2350;
            glebokosc = 6;
            numerSeryjny = "KON-"+EstablishConType()+"-"+GenerateSN();
            maxLadownosc = 25_000;
        }
        public virtual void EmptyLoad()
        {
            masaLadunku = WagaWlasna;
        }
        public virtual void Load(int masaDoZaladowania)
        {
            if (masaLadunku + WagaWlasna +masaDoZaladowania  <= maxLadownosc)
            {
                masaLadunku += masaDoZaladowania;
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

        private int GenerateSN()
        {
             return new Random().Next(1, 9999);
        }

        private string EstablishConType()
        {
            if (this is KontenerNaPlyny)
            {
                 return "L";
            }
            if (this is KontenernaGaz)
            {
                return "G";
            }
            if (this is KontenerChlodniczy)
            {
                return "C";
            }
            return "N";
        }

        public override string ToString()
        {
            return "Numer seryjny: "+numerSeryjny+" Masa ladunku: "+masaLadunku;
        }
        public virtual String GetInfo()
        {
            return "Numer seryjny: "+numerSeryjny+" Masa ladunku: "+masaLadunku+" Wysokosc: "+wysokosc+" Glebokosc: "+glebokosc+" Waga wlasna: "+WagaWlasna+" Maksymalna ladownosc: "+maxLadownosc+" Czy jest niebezpieczny: "+isHazardous;
        }
    }

    internal class OverFilledException : Exception
    {
        public OverFilledException()
        {
            Console.WriteLine("Kontener jest przeciażony!");
        }
    }

    class KontenerNaPlyny : Kontener,IHazardNotifier
    {
        public KontenerNaPlyny(String setHazardus) : base()
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

        public override void Load(int masaDoZaladowania)
        {
            if (GetIsHazardous())
            {
                SetmaxLadownosc(GetmaxLadownosc() * 0.5);
            }
            else
            {
                SetmaxLadownosc(GetmaxLadownosc() * 0.9);
            }

            if (GetMasaLadunku()+GetWagaWlasna()>GetmaxLadownosc())
            {
                IHazardNotifier.NotifyHazard(GetNumerSeryjny());
            }
            
            base.Load(masaDoZaladowania);
        }
    }

    class KontenernaGaz : Kontener,IHazardNotifier
    {
        private int Pressure;
        
        public KontenernaGaz() : base()
        {
            Pressure = new Random().Next(1, 100);
        }

        public override void EmptyLoad()
        {
            SetMasaLadunku(GetMasaLadunku() * 0.05);
        }
    }
    class KontenerChlodniczy : Kontener
    {
        private double temp;
        private TypProduktu typProdukt { get; set; }
        
        
        public KontenerChlodniczy(TypProduktu typProduktu) : base()
        {
            this.typProdukt = typProduktu;
            UstalTemp(typProduktu);
        }

        private void UstalTemp(TypProduktu typProduktu)
        {
            switch (typProduktu)
            {
                case TypProduktu.Bananas:
                    temp = 13.5;
                    break;
                case TypProduktu.Chocolate:
                    temp = 18;
                    break;
                case TypProduktu.Fish:
                    temp = 2;
                    break;
                case TypProduktu.Meat:
                    temp = -15;
                    break;
                case TypProduktu.IceCream:
                    temp = -18;
                    break;
                case TypProduktu.FrozenPizza:
                    temp = -30;
                    break;
                case TypProduktu.Cheese:
                    temp = 7.2;
                    break;
                case TypProduktu.Sausages:
                    temp = 5;
                    break;
                case TypProduktu.Butter:
                    temp = 20.5;
                    break;
                case TypProduktu.Eggs:
                    temp = 19;
                    break;
            }
        }
    }

    public interface IHazardNotifier
    {
        public static void NotifyHazard(String sn)
        {
            Console.WriteLine("Uwaga! Kontener zawiera materiały niebezpieczne!"+" Numer seryjny: "+sn);
        }
    }

    enum TypProduktu
    {
        Bananas,Chocolate,Fish,Meat,IceCream,FrozenPizza,Cheese,Sausages,Butter,Eggs
    }
    
}

namespace kontenerowce
{
   public class Kontenerowiec
    {
        public static int number = 0;
        private string identifikator;
        private List<Kontener> konteners; 
        private int maxpredkosc;
        private int maxiloscKontenerow;
        private int maxLadownosc;

        public Kontenerowiec()
        {
            maxpredkosc = new Random().Next(1,25);
            maxiloscKontenerow = new Random().Next(10_000,24_000);
            maxLadownosc = 24_000*25_000;
            konteners = new List<Kontener>();
            identifikator = "SHIP-"+number;
            number++;
        }
        public void DodajKonetner(Kontener kontener)
        {
            konteners.Add(kontener);
        }

        public override string ToString()
        {
            return "Identyfikator: "+identifikator+" Maksymalna predkosc: "+maxpredkosc+" Maksymalna ilosc kontenerow: "+maxiloscKontenerow+" Maksymalna ladownosc: "+maxLadownosc+" Aktualna ilosc kontenerow: "+konteners.Count;
        }
        public String getIdentifikator()
        {
            return identifikator;
        }
        public List<Kontener> getKonteners()
        {
            return konteners;
        }
    }
    
}