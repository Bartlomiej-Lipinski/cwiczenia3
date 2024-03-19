using System.Collections;

namespace Kontenery
{
    abstract class Kontener
    {
        private double masaLadunku;
        private int wysokosc;
        private double WagaWlasna;
        private int glebokosc;
        private string numerSeryjny;
        private double maxLadownosc;
        private bool isHazardous;

        public Kontener()
        {
            masaLadunku = WagaWlasna + masaLadunku ;
            wysokosc = 3;
            WagaWlasna = 2350;
            glebokosc = 6;
            numerSeryjny = "KON-"+establishConType()+"-"+generateSN();
            maxLadownosc = 25_000;
        }
        public void OprozniLadunek()
        {
            masaLadunku = WagaWlasna;
        }
        public virtual void zaladujLadunek(int masaDoZaladowania)
        {
            if (masaLadunku + WagaWlasna +masaDoZaladowania  <= maxLadownosc)
            {
                masaLadunku += masaDoZaladowania;
            }
            else
            {
                throw new OverFilledException();
            }
        }

        private int generateSN()
        {
             return new Random().Next(1, 9999);
        }

        private string establishConType()
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
        public KontenerNaPlyny() : base()
        {
            
        }

        public override void zaladujLadunek(int masaDoZaladowania)
        {
            base.zaladujLadunek(masaDoZaladowania);
        }
        
    }

    class KontenernaGaz : Kontener,IHazardNotifier
    {
        int Pressure { get; set; }
        
        public KontenernaGaz() : base()
        {
            
        }
        
    }
    class KontenerChlodniczy : Kontener
    {
        private int temp { get;set; }
        private TypProduktu typProdukt { get; set; }
        
        public KontenerChlodniczy(TypProduktu typProduktu) : base()
        {
            this.typProdukt = typProduktu;
        }
    }

    public interface IHazardNotifier
    {
        private void NotifyHazard(String sn)
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
    class Kontenerowiec
    {
        private ArrayList kontenery = new ArrayList();
        private int maxpredkosc;
        private int maxiloscKontenerow;
        private int maxLadownosc;

        public Kontenerowiec(int maxpredkosc, int maxiloscKontenerow, int maxLadownosc)
        {
            this.maxpredkosc = maxpredkosc;
            this.maxiloscKontenerow = maxiloscKontenerow;
            this.maxLadownosc = maxLadownosc;
        }
    }
}
